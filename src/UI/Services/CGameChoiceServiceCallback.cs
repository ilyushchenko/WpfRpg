using System;
using Core.Data;
using UI.GameServer;

namespace UI.Services
{
    public class CGameChoiceServiceCallback : IGameChoiceServiceCallback
    {
        public void PlayerHasConnected(CPlayer player)
        {
            PlayerConnected?.Invoke(this, player);
        }

        public void PlayerHasDisconnected(CPlayer player)
        {
            PlayerDisconnected?.Invoke(this, player);
        }

        public void GameHasStarted(String gameUri)
        {
            GameStarted?.Invoke(this, gameUri);
        }

        public event EventHandler<CPlayer> PlayerConnected;
        public event EventHandler<CPlayer> PlayerDisconnected;
        public event EventHandler<String> GameStarted;
    }
}