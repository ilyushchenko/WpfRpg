using UI.GameServer;
using UI.Interfaces;

namespace UI.Services
{
    internal class CMapServiceProvider : IMapServiceProvider
    {
        private CMapServiceProvider()
        {
            Service = new MapServiceClient("NetTcpBinding_IMapService");
        }

        public MapServiceClient Service { get; }

        public static CMapServiceProvider Create()
        {
            return new CMapServiceProvider();
        }
    }
}