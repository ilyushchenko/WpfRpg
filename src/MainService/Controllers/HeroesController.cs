using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BusinessLayer.Providers;
using Core.Data;
using Core.Models.Heroes;
using Interfaces.Enums;
using MainService.Models;

namespace MainService.Controllers
{
    [RoutePrefix("api/heroes")]
    public class HeroesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<CHeroData> Get()
        {
            CHeroesProvider provider = CHeroesProvider.Create();
            return provider.GetHeroes();
        }

        [HttpGet]
        [Route("{type}")]
        public CHeroData GetHeroByType(EHeroTypes type)
        {
            CHeroesProvider provider = CHeroesProvider.Create();
            return provider.GetHero(type);
        }
    }
}