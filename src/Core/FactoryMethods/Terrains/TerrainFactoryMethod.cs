using System;
using Core.Models.Terrains;
using Interfaces;
using Interfaces.Enums;

namespace Core.FactoryMethods.Terrains
{
    public class CTerrainFactoryMethod
    {
        public static ITerrain Create(ETerrainTypes type)
        {
            switch (type)
            {
                case ETerrainTypes.Dirt:
                    return new CDirt();
                case ETerrainTypes.Grass:
                    return new CGrass();
                case ETerrainTypes.Water:
                    return new CWater();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, $"Terrain {type} not found");
            }
        }
    }
}