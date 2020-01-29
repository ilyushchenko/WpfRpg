using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Core.Data;
using Core.Models;
using Core.Models.Heroes;
using Core.Models.Units;
using GameServer.Contracts;
using Interfaces;
using Interfaces.Enums;

namespace GameServerWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GameService : IGameService
    {
        private readonly CServerMap _serverMap;
        private readonly List<CServerPlayer> _players;
        private Int32 _round;
        private Timer _roundTimer;
        private Timer _endRoundTimer;
        private TimeSpan _roundTime;

        public GameService(CMap map, IEnumerable<CPlayer> players)
        {
            _serverMap = new CServerMap(map);
            _players = new List<CServerPlayer>(
                players.Select(p => new CServerPlayer {PlayerInfo = p, Status = EGameStatus.Connecting}));
            _round = 0;
            _roundTimer = new Timer(OnTimerEnded);
            _endRoundTimer = new Timer(OnEndRoundCountdownDone);
            _roundTime = TimeSpan.FromMinutes(1);
        }

        private void OnEndRoundCountdownDone(Object state)
        {
            StartNewRound();
        }

        private void OnTimerEnded(Object state)
        {
            TimeSpan countdown = TimeSpan.FromSeconds(10);
            foreach (CServerPlayer playerCallback in _players)
            {
                playerCallback.Callback.RoundHasEnded(countdown);
            }

            _endRoundTimer.Change(countdown, TimeSpan.FromMilliseconds(-1));
        }

        public String Connect(Guid playerId)
        {
            //TODO: Check duplicates
            var callback = OperationContext.Current.GetCallbackChannel<IGameServiceCallback>();
            String session = OperationContext.Current.SessionId;

            CServerPlayer player = _players.Single(p => p.PlayerInfo.Id == playerId);
            player.Callback = callback;
            player.Session = session;
            player.Status = EGameStatus.PlayerSelecting;
            return session;
        }

        public void FinishRound(String session)
        {
        }

        public void SelectHero(CHeroBase hero)
        {
            CServerPlayer player = GetPlayer();
            player.Hero = hero;
            player.Status = EGameStatus.PlayerSelected;
            if (_players.All(p => p.Status == EGameStatus.PlayerSelected))
            {
                var i = 0;
                CMap map = _serverMap.GetMap();
                foreach (CServerPlayer playerCallback in _players)
                {
                    i++;
                    _serverMap.Spawn(playerCallback.Hero, i, i);
                }

                foreach (CServerPlayer playerCallback in _players)
                {
                    playerCallback.Callback.GameHasStarted(map, playerCallback.Hero);
                }

                StartNewRound();
            }
        }

        private void StartNewRound()
        {
            _round++;
            foreach (CServerPlayer playerCallback in _players)
            {
                playerCallback.Callback.RoundHasStarted(_round, _roundTime);
            }

            _roundTimer.Change(_roundTime, TimeSpan.FromMilliseconds(-1));
        }

        public Boolean TryMoveUnit(SPoint position, SPoint newPosition)
        {
            Boolean transferResult = _serverMap.TryMove(position, newPosition, out CHeroBase hero);

            if (transferResult)
            {
                String session = OperationContext.Current.SessionId;

                Task.Run(() =>
                {
                    foreach (CServerPlayer playerCallback in _players.Where(player => player.Session != session))
                    {
                        playerCallback.Callback.HeroHasMoved(hero, position, newPosition);
                    }
                });
            }

            return transferResult;
        }

        public void KillUnit(SPoint position)
        {
            String session = OperationContext.Current.SessionId;
            Task.Run(() =>
            {
                foreach (CServerPlayer playerCallback in _players.Where(player => player.Session != session))
                {
                    playerCallback.Callback.UnitHasDied(position);
                }
            });
        }

        private CServerPlayer GetPlayer()
        {
            String session = OperationContext.Current.SessionId;

            return _players.SingleOrDefault(p => p.Session == session);
        }
    }
}