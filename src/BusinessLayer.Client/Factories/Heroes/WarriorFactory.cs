using System;
using System.Collections.Generic;
using Core.Models.Heroes;
using Core.Models.Items;
using Interfaces;

namespace BusinessLayer.Client.Factories.Heroes
{
    internal class CWarriorFactory : CHeroFactory
    {
        protected override CHeroBase GetHero()
        {
            return CWarrior.Create("Warrior", 300, 15, 50);
        }

        protected override IWeapon GetWeapon()
        {
            return CWeapon.Create(Guid.Parse("DC1430DA-39F0-41BF-AA50-F185ACF951A6"), "Warrior weapon", 100, 200);
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