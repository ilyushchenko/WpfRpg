using System;
using System.Collections.Generic;
using BusinessLayer.Client.Factories.Heroes;
using Core.Data;
using Core.Models.Heroes;
using Interfaces.Enums;

namespace BusinessLayer.Client.Providers
{
    public class CHeroesProvider
    {
        private CHeroesProvider()
        {
        }

        public IEnumerable<CHeroData> GetHeroes()
        {
            var data = SHelper.Get<CHeroData[]>("api/heroes");
            return data;
        }

        public CHeroBase GetHero(EHeroTypes type)
        {
            CHeroFactory heroFactory = null;
            switch (type)
            {
                case EHeroTypes.Paladin:
                    heroFactory = new CPaladinFactory();
                    break;
                case EHeroTypes.Mage:
                    heroFactory = new CMageFactory();
                    break;
                case EHeroTypes.Thief:
                    heroFactory = new CThiefFactory();
                    break;
                case EHeroTypes.Warrior:
                    heroFactory = new CWarriorFactory();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return heroFactory.CreateHero();
        }

        public static CHeroesProvider Create()
        {
            return new CHeroesProvider();
        }
    }
}