using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Core.FactoryMethods.Terrains;
using Core.FactoryMethods.Units;
using Core.Models;
using Interfaces;
using Interfaces.Enums;

namespace BusinessLayer.MapLoaders
{
    public class CXmlMapLoader : IMapLoader
    {
        private readonly XElement _mapData;

        private CXmlMapLoader(XDocument document)
        {
            _mapData = document.Element("mapData");
        }

        public Int32 GetWidth()
        {
            XElement map = GetMap();
            String value = map?.Element("width")?.Value ?? throw new Exception("Width attribute not found");
            return Int32.Parse(value);
        }

        public Int32 GetHeight()
        {
            XElement map = GetMap();
            String value = map?.Element("height")?.Value ?? throw new Exception("Height attribute not found");
            return Int32.Parse(value);
        }

        public String GetName()
        {
            XElement map = GetMap();
            return map?.Element("name")?.Value ?? throw new Exception("Name attribute not found");
        }

        public ICell GetCell(SPoint position)
        {
            var cell = new CCell(position.X, position.Y);

            XElement cellData = GetCellElement(position);

            String terrainType = cellData?.Attribute("terrain")?.Value;

            cell.Terrain = GetTerrain(terrainType);

            return cell;
        }

        public IPositionable GetUnit(SPoint position)
        {
            XElement cellData = GetCellElement(position);
            XElement unitData = cellData.Element("unit");
            return unitData != null ? CreateUnit(unitData) : null;
        }

        public static CXmlMapLoader Create(String path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) throw new FileNotFoundException($"XML map file({path}) not found");
            XDocument document = XDocument.Load(path);
            return new CXmlMapLoader(document);
        }

        private static Boolean IsCellPosition(XElement c, SPoint position)
        {
            Int32 xPosition = Int32.Parse(c.Attribute("x")?.Value ??
                                          throw new Exception("Element cell must have attribute 'x'"));
            Int32 yPosition = Int32.Parse(c.Attribute("y")?.Value ??
                                          throw new Exception("Element cell must have attribute 'y'"));
            return xPosition == position.X && yPosition == position.Y;
        }

        private ITerrain GetTerrain(String terrainTypeName)
        {
            if (terrainTypeName == null)
            {
                XElement cells = GetCells();
                terrainTypeName = cells.Attribute("defaultTerrain")?.Value ??
                              throw new Exception("Element cells must have attribute 'defaultTerrain'");
            }

            IEnumerable<XElement> terrains = GetTerrains().Elements("terrain");

            XElement terrainData = terrains.SingleOrDefault(t => t.Attribute("type")?.Value == terrainTypeName);

            if (terrainData == null) throw new Exception($"Terrain {terrainTypeName} not found");

            String type = terrainData.Attribute("type")?.Value;

            var terrainType = (ETerrainTypes)Enum.Parse(typeof(ETerrainTypes), type, true);

            ITerrain terrain = CTerrainFactoryMethod.Create(terrainType);

            return terrain;
        }

        private XElement GetCellElement(SPoint position)
        {
            XElement cells = GetCells();
            XElement cell = cells.Elements("cell").Single(c => IsCellPosition(c, position));
            return cell;
        }

        private IPositionable CreateUnit(XElement unitData)
        {
            String type = unitData.Attribute("type")?.Value;

            var unitType = (EUnitTypes)Enum.Parse(typeof(EUnitTypes), type, true);

            CUnitFactoryMethod unitFactoryMethod = CUnitFactoryMethod.GetFactory(unitType);
            return unitFactoryMethod.CreateUnit(unitData);
        }

        #region Root elements

        private XElement GetElementOrThrow(String elementName)
        {
            XElement cells = _mapData.Element(elementName) ??
                             throw new Exception($"Element {elementName} not found in mapData");
            return cells;
        }

        private XElement GetCells()
        {
            return GetElementOrThrow("cells");
        }

        private XElement GetMap()
        {
            return GetElementOrThrow("map");
        }

        private XElement GetTerrains()
        {
            return GetElementOrThrow("terrains");
        }

        #endregion
    }
}