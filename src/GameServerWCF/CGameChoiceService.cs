using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.MapLoaders;
using BusinessLayer.Providers;
using Core.Data;
using Core.Models;
using GameServer.Contracts;
using GameServerWCF.Interfaces.GameChoiceService;
using GameServerWCF.Interfaces.MapService;
using Interfaces;
using NLog;

namespace GameServerWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CGameChoiceService : IGameChoiceService, IMapService
    {
        private readonly Object _gamesListSync = new Object();
        private Int32 _nextPort;
        private readonly List<ServiceHost> _gameServiceHosts;
        private readonly List<CGame> _games;
        private readonly Timer _scanningTimer;
        private readonly List<CGamePlayer> _players;
        private readonly CMapDatabaseProvider _mapProvider;
        private readonly ILogger _logger;

        private readonly Dictionary<String, IContextChannel> _sessions = new Dictionary<String, IContextChannel>();

        public CGameChoiceService()
        {
            //TODO: Insert to config
            _nextPort = 7800;
            _gameServiceHosts = new List<ServiceHost>();
            _players = new List<CGamePlayer>();
            _games = new List<CGame>();
            _logger = LogManager.GetCurrentClassLogger();
            _scanningTimer = new Timer(CheckReadyGames, null, 1000, 0);
            _mapProvider = CMapDatabaseProvider.Create();
        }

        private void CheckReadyGames(Object state)
        {
            CGame[] readyGames = _games.Where(g => g.CheckGameIsReady()).ToArray();
            foreach (CGame readyGame in readyGames)
            {
                _logger.Info($"Game {readyGame.Name}({readyGame.Id}) is ready");
                ServiceHost gameHost = Task.Run(() => StartGame(readyGame)).Result;
                Uri url = gameHost.BaseAddresses.First();

                readyGame.Start(url);
                _logger.Info($"Game {readyGame.Name}({readyGame.Id}) started on URL {url}");
                _games.Remove(readyGame);
            }


            _scanningTimer.Change(1000, 0);
        }

        private ServiceHost StartGame(CGame game)
        {
            Int32 usedPort = _nextPort++;
            String serviceUrl = $"net.tcp://localhost:{usedPort}";
            var urls = new[] {new Uri(serviceUrl)};

            IMapLoader mapLoader = CXmlMapLoader.Create("C:\\Users\\Pavel\\Pavel.Ilushenko\\OwnProject\\BusinessLayer\\TestMap.xml");
            CMap map = CMap.Create(mapLoader);

            IGameService service = new GameService(map, game.GetPlayers().Select(p => p.PlayerInfo));

            var host = new ServiceHost(service, urls);
            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(IGameService), binding, String.Empty);
            host.Opened += HostOnOpened;
            host.Open();
            _gameServiceHosts.Add(host);

            return host;
        }

        private void HostOnOpened(Object sender, EventArgs e)
        {
            if (sender is ServiceHost host)
            {
                Logger.WriteLine($"Game server started at {DateTime.Now} UTC");

                foreach (Uri hostBaseAddress in host.BaseAddresses)
                {
                    Logger.WriteLine($"Available on {hostBaseAddress.AbsoluteUri}");
                }
            }
        }

        public CGameInfo[] GetGames()
        {
            return _games.Select(g => g.GetInfo()).ToArray();
        }

        public void Ready(Guid gameId)
        {
            CGame game = _games.SingleOrDefault(g => g.Id == gameId);
            String session = OperationContext.Current.SessionId;
            game?.Ready(session);
        }

        public void Disconnect(Guid gameId)
        {
            String session = OperationContext.Current.SessionId;
            DisconnectInternal(gameId, session);
        }

        private void DisconnectInternal(Guid gameId, String session)
        {
            CGame game = _games.SingleOrDefault(g => g.Id == gameId);
            game?.Disconnect(session);
        }

        public CGameInfo CreateGame(String name, Int32 maxPlayers)
        {
            if (maxPlayers < 1) throw new ArgumentException("Player numbers must be 1 or more", nameof(maxPlayers));
            var game = new CGame(name, maxPlayers);
            _games.Add(game);

            TryConnect(game.Id, out CGameInfo gameInfo);
            return gameInfo;
        }

        public Boolean TryConnect(Guid gameId, out CGameInfo gameInfo)
        {
            gameInfo = null;

            CGame game = _games.FirstOrDefault(g => g.Id == gameId);
            if (game == null) return false;

            String session = OperationContext.Current.SessionId;
            CGamePlayer gamePlayer = _players.Single(p => p.PlayerInfo.Session == session);

            Boolean connectionResult = game.TryConnect(gamePlayer);
            if (connectionResult)
            {
                gameInfo = game.GetInfo();
            }

            return connectionResult;
        }

        public Boolean LogIn(String login, Guid id, out CPlayer player)
        {
            IContextChannel chanel = OperationContext.Current.Channel;
            String session = OperationContext.Current.SessionId;
            chanel.Faulted += ChanelOnFaulted;

            player = new CPlayer(session, login, id);
            var gamePlayer = new CGamePlayer(player);

            var callback = OperationContext.Current.GetCallbackChannel<IGameChoiceServiceCallback>();
            gamePlayer.Callback = callback;

            _players.Add(gamePlayer);
            return true;
        }

        private void ChanelOnFaulted(Object sender, EventArgs e)
        {
            if (sender is IContextChannel chanel)
            {
                String session = chanel.SessionId;
                CGamePlayer player = _players.FirstOrDefault(p => p.PlayerInfo.Session == session);
                if (player != null)
                {
                    if (player.Game != null)
                    {
                        Guid gameId = player.Game.Id;
                        DisconnectInternal(gameId, session);
                    }

                    _players.Remove(player);
                }
            }
        }

        public IEnumerable<CMapInfo> GetMaps()
        {
            return _mapProvider.GetMapsInfo();
        }
    }

    public static class Logger
    {
        public static void WriteLine(String text)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}\t|\t{text}");
        }
    }
}