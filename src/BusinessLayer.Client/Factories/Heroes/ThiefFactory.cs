using System;
using System.Collections.Generic;
using Core.Models.Heroes;
using Core.Models.Items;
using Interfaces;

namespace BusinessLayer.Client.Factories.Heroes
{
    internal class CThiefFactory : CHeroFactory
    {
        protected override CHeroBase GetHero()
        {
            return CThief.Create("Thief ", 100, 30, 45);
        }

        protected override IWeapon GetWeapon()
        {
            return CWeapon.Create(Guid.Parse("DC1430DA-39F0-41BF-AA50-F185ACF951A6"), "Thief weapon", 100, 2200);
        }

        protected override IList<IInventoryItem> GetItems()
        {
            return new List<IInventoryItem>()
            {
                new CHealthPotion(),
                new CHealthPotion(),
                new CHealthPotion(),
            };
        }
    }
}