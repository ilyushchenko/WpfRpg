using System;
using System.Collections.Generic;
using Core.Models.Heroes;
using Core.Models.Items;
using Interfaces;

namespace BusinessLayer.Client.Factories.Heroes
{
    internal class CPaladinFactory : CHeroFactory
    {
        protected override CHeroBase GetHero()
        {
            return CPaladin.Create("Paladin ", 100, 30, 45);
        }

        protected override IWeapon GetWeapon()
        {
            return CWeapon.Create(Guid.Parse("DC1430DA-39F0-41BF-AA50-F185ACF951A6"), "Paladin weapon", 100,100);
        }

        protected override IList<IInventoryItem> GetItems()
        {
            return new List<IInventoryItem>()
            {
                new CHealthPotion(),
                new CHealthPotion(),
            };
        }
    }
}
