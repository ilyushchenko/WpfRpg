using System.Collections.Generic;
using System.Linq;
using Core.Data;
using DataAccessLayer.DTO;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Providers
{
    public class CMapDatabaseProvider
    {
        private readonly CMapsRepository _mapsRepository;

        private CMapDatabaseProvider()
        {
            _mapsRepository = CMapsRepository.GetRepository();
        }

        public IEnumerable<CMapInfo> GetMapsInfo()
        {
            IEnumerable<CMapInfo> maps = _mapsRepository.GetAll().Select(CreteMapFromDto);
            return maps;
        }

        private CMapInfo CreteMapFromDto(CMapDto mapDto)
        {
            return CMapInfo.Create(mapDto.Id, mapDto.Name, mapDto.Width, mapDto.Height);
        }

        public static CMapDatabaseProvider Create()
        {
            return new CMapDatabaseProvider();
        }
    }
}