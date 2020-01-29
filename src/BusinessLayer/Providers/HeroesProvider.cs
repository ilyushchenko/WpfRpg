using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;
using DataAccessLayer.DTO;
using DataAccessLayer.Repositories;
using Interfaces.Enums;

namespace BusinessLayer.Providers
{
    public class CHeroesProvider
    {
        private readonly CHeroesRepository _heroesRepository;

        private CHeroesProvider(CHeroesRepository heroesRepository)
        {
            _heroesRepository = heroesRepository;
        }

        public IEnumerable<CHeroData> GetHeroes()
        {
            IEnumerable<CHeroDto> heroesDto = _heroesRepository.GetAll();
            return heroesDto.Select(MapHeroDataFromDto);
        }

        public CHeroData GetHero(EHeroTypes type)
        {
            CHeroDto heroDto = _heroesRepository.GetByType((Int32) type);
            return MapHeroDataFromDto(heroDto);
        }

        private static CHeroData MapHeroDataFromDto(CHeroDto heroDto)
        {
            var type = (EHeroTypes) heroDto.Type;
            return CHeroData.Create(heroDto.Name, type, heroDto.Health, heroDto.MovingEnergy, heroDto.Description);
        }

        public static CHeroesProvider Create()
        {
            CHeroesRepository heroesRepository = CHeroesRepository.GetRepository();
            return new CHeroesProvider(heroesRepository);
        }
    }
}