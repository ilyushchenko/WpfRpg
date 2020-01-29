using System.Collections.Generic;
using Core.Models.Heroes;
using Interfaces;

namespace BusinessLayer.Client.Factories.Heroes
{
    internal abstract class CHeroFactory
    {
        protected abstract CHeroBase GetHero();

        protected abstract IWeapon GetWeapon();

        protected abstract IList<IInventoryItem> GetItems();

        public CHeroBase CreateHero()
        {
            CHeroBase hero = GetHero();
            IList<IInventoryItem> items = GetItems();
            hero.AddToInventory(items);

            IWeapon weapon = GetWeapon();
            hero.AddToInventory(weapon);

            return hero;
        }
    }
}