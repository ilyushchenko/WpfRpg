using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core.FactoryMethods.Items;
using Core.Models.Units;
using Interfaces;
using Interfaces.Enums;

namespace Core.FactoryMethods.Units
{
    public class CTraderFactoryMethod : CUnitFactoryMethod
    {
        private CTraderFactoryMethod()
        {
        }

        public override IPositionable CreateUnit(XElement traderData)
        {
            if (traderData == null) throw new ArgumentNullException(nameof(traderData));

            XElement traderItemsData = traderData.Element("items");

            var traderItems = new List<IInventoryItem>();

            if (traderItemsData != null)
                traderItems = traderItemsData.Elements("item")
                    .Select(itemElement =>
                    {
                        String type = itemElement.Attribute("type")?.Value;

                        var itemType = (EItemTypes) Enum.Parse(typeof(EItemTypes), type, true);
                        CItemFactoryMethod itemFactory = CItemFactoryMethod.GetFactory(itemType);

                        return itemFactory.Create(itemElement);
                    })
                    .ToList();


            var trader = new CTrader(traderItems);
            return trader;
        }

        public static CTraderFactoryMethod GetFactory()
        {
            return new CTraderFactoryMethod();
        }
    }
}