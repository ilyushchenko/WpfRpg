using System;
using System.ServiceModel;
using GameServer.Contracts;
using UI.Interfaces;

namespace UI.Services
{
    public class CGameServiceProvider : IGameServiceProvider
    {
        private CGameServiceProvider(IGameService gameService, СGameServiceCallback callback)
        {
            GameClient = gameService;
            ServiceCallback = callback;
        }

        public IGameService GameClient { get; }
        public СGameServiceCallback ServiceCallback { get; }

        public static CGameServiceProvider Create(String gameServerUrl)
        {
            var callback = new СGameServiceCallback();
            var callbackInstance = new InstanceContext(callback);
            var binding = new NetTcpBinding(SecurityMode.None);
            var chanel = new DuplexChannelFactory<IGameService>(callback, binding);
            var endPoint = new EndpointAddress(gameServerUrl);
            IGameService proxy = chanel.CreateChannel(endPoint);

            return new CGameServiceProvider(proxy, callback);
        }
    }
}