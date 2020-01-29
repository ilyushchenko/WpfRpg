using System.Collections.Generic;
using System.ServiceModel;
using Core.Data;

namespace GameServerWCF.Interfaces.MapService
{
    [ServiceContract]
    public interface IMapService
    {
        [OperationContract]
        IEnumerable<CMapInfo> GetMaps();
    }
}
