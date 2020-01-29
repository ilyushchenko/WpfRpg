using System;
using System.Xml.Linq;
using Interfaces;
using Interfaces.Enums;

namespace Core.FactoryMethods.Items
{
    public abstract class CItemFactoryMethod
    {
        public abstract IInventoryItem Create(XElement itemData);
        public abstract IInventoryItem CreateFromJson(String itemDataJson);

        public static CItemFactoryMethod GetFactory(EItemTypes type)
        {
            switch (type)
            {
                case EItemTypes.Weapon: return new CWeaponFactoryMethod();
                case EItemTypes.HealthPotion: return new CHealthPotionFactoryMethod();
                case EItemTypes.XpBook: return new CXpBookFactoryMethod();
                default: throw new Exception($"Factory for type {type} not found");
            }
        }
    }
}