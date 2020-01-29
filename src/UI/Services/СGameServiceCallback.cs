using System;
using Core.Models;
using Core.Models.Heroes;
using GameServer.Contracts;
using Interfaces;

namespace UI.Services
{
    public class СGameServiceCallback : IGameServiceCallback
    {
        public void GameHasStarted(CMap map, CHeroBase hero)
        {
            GameStarted?.Invoke(this, (map, hero));
        }

        public void RoundHasStarted(Int32 roundNumber, TimeSpan roundTime)
        {
            RoundStarted?.Invoke(this, (Round: roundNumber, RoundTime: roundTime));
        }

        public void PlayerHasMoved()
        {
        }

        public void RoundHasEnded(TimeSpan timeout)
        {
            RoundEnded?.Invoke(this, timeout);
        }

        public void GameHasEnded()
        {
            GameEnded?.Invoke(this, EventArgs.Empty);
        }

        public void HeroHasMoved(CHeroBase hero, SPoint oldPosition, SPoint newPosition)
        {
            HeroMoved?.Invoke(this, (Hero: hero, OldPosition: oldPosition, NewPosition: newPosition));
        }

        public void UnitHasDied(SPoint position)
        {
            UnitDied?.Invoke(this, position);
        }

        public event EventHandler<(CMap, CHeroBase)> GameStarted;
        public event EventHandler<(CHeroBase Hero, SPoint OldPosition, SPoint NewPosition)> HeroMoved;
        public event EventHandler<(Int32 Round ,TimeSpan RoundTime)> RoundStarted;
        public event EventHandler<TimeSpan> RoundEnded;
        public event EventHandler GameEnded;
        public event EventHandler<SPoint> UnitDied;
    }
}