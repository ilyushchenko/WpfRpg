using System;
using System.Xml.Linq;
using Interfaces;
using Interfaces.Enums;

namespace Core.FactoryMethods.Units
{
    public abstract class CUnitFactoryMethod
    {
        public abstract IPositionable CreateUnit(XElement unitData);

        public static CUnitFactoryMethod GetFactory(EUnitTypes type)
        {
            switch (type)
            {
                case EUnitTypes.Trader:
                    return CTraderFactoryMethod.GetFactory();
                case EUnitTypes.Wall:
                    return CWallFactoryMethod.GetFactory();
                case EUnitTypes.Monster:
                    return new CMonsterFactoryMethod();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, $"Factory for type {type} not found");
            }
        }
    }
}