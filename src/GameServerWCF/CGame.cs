using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Data;
using Core.Models;
using GameServerWCF.Interfaces.GameChoiceService;
using NLog;

namespace GameServerWCF
{
    public class CGame
    {
        private readonly ILogger _logger;
        private readonly List<CGamePlayer> _players;
        private readonly Semaphore _playersSemaphore;
        private readonly Object _threadSync = new Object();

        public CGame(String name, Int32 maxPlayers)
        {
            _logger = LogManager.GetCurrentClassLogger();
            Id = Guid.NewGuid();
            Name = name;
            _players = new List<CGamePlayer>();
            MaxPlayers = maxPlayers;
            _playersSemaphore = new Semaphore(MaxPlayers, MaxPlayers);
        }

        public CMap Map { get; }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 MaxPlayers { get; set; }

        public Boolean CheckGameIsReady()
        {
            lock (_threadSync)
            {
                return _players.Count == MaxPlayers && _players.All(p => p.Status == EPlayerStatus.InLobby);
            }
        }

        public IEnumerable<CGamePlayer> GetPlayers()
        {
            lock (_threadSync)
            {
                return _players.AsReadOnly();
            }
        }

        public Boolean TryConnect(CGamePlayer player)
        {
            _logger.Info($"Player session {player.PlayerInfo.Session} try connect to game {Id}");
            Boolean exitResult = _playersSemaphore.WaitOne(TimeSpan.FromSeconds(10));
            if (!exitResult)
            {
                _logger.Info($"Player with session {player.PlayerInfo.Session} not connected to game {Id} by timeout");
                return false;
            }
            lock (_threadSync)
            {
                _players.Add(player);
                player.Game = this;
            }

            NotifyWithout(cb => cb.PlayerHasConnected(player.PlayerInfo), player);
            _logger.Info($"Player session {player.PlayerInfo.Session} connected to game {Id}");

            return true;
        }

        public void Ready(String session)
        {
            lock (_threadSync)
            {
                CGamePlayer player = _players.Single(p => p.PlayerInfo.Session == session);
                _logger.Info($"Game {Id} - Player with session {player.PlayerInfo.Session} Ready");
                player.Status = EPlayerStatus.InLobby;
            }
        }

        public void Start(Uri url)
        {
            NotifyAll(cb => cb.GameHasStarted(url.AbsoluteUri));
        }

        public void Disconnect(String session)
        {
            CGamePlayer player;
            lock (_threadSync)
            {
                player = _players.Single(p => p.PlayerInfo.Session == session);
                _players.Remove(player);
                player.Game = null;
                player.Status = EPlayerStatus.NotPlaying;
            }

            NotifyAll(cb => cb.PlayerHasDisconnected(player.PlayerInfo));

            _playersSemaphore.Release();
            _logger.Info($"Player with session {player.PlayerInfo.Session} disconnected from game {Id}");
        }

        public CGameInfo GetInfo()
        {
            lock (_threadSync)
            {
                return new CGameInfo
                {
                    Id = Id,
                    MaxPlayers = MaxPlayers,
                    Name = Name,
                    CurrentPlayersCount = _players.Count,
                    Players = _players.Select(pd => pd.PlayerInfo).ToList()
                };
            }
        }

        private void NotifyAll(Action<IGameChoiceServiceCallback> action)
        {
            lock (_threadSync)
            {
                foreach (CGamePlayer notifyPlayer in _players)
                {
                    action(notifyPlayer.Callback);
                }
            }
        }

        private void NotifyWithout(Action<IGameChoiceServiceCallback> action, CGamePlayer player)
        {
            lock (_threadSync)
            {
                foreach (CGamePlayer notifyPlayer in _players.Where(p => p != player))
                {
                    action(notifyPlayer.Callback);
                }
            }
        }
    }
}