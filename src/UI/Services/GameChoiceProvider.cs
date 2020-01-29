using System;
using System.ServiceModel;
using UI.GameServer;
using UI.Interfaces;

namespace UI.Services
{
    internal class GameChoiceProvider : IGameChoiceProvider
    {
        private static readonly Lazy<IGameChoiceProvider> _instance = new Lazy<IGameChoiceProvider>(Create);

        private GameChoiceProvider()
        {
            Callback = new CGameChoiceServiceCallback();
            Service = new GameChoiceServiceClient(new InstanceContext(Callback), "NetTcpBinding_IGameChoiceService");
        }

        public static IGameChoiceProvider Instance => _instance.Value;

        public GameChoiceServiceClient Service { get; }
        public CGameChoiceServiceCallback Callback { get; }

        private static IGameChoiceProvider Create()
        {
            return new GameChoiceProvider();
        }
    }
}