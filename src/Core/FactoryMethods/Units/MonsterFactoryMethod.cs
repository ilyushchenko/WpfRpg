using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Core.FactoryMethods.Items;
using Core.Models.Units;
using Interfaces;
using Interfaces.Enums;

namespace Core.FactoryMethods.Units
{
    public class CMonsterFactoryMethod : CUnitFactoryMethod
    {
        public override IPositionable CreateUnit(XElement traderData)
        {
            if (traderData == null) throw new ArgumentNullException(nameof(traderData));

            XElement itemsData = traderData.Element("items");

            var traderItems = new List<IInventoryItem>();

            if (itemsData != null)
                traderItems = itemsData.Elements("item")
                    .Select(itemElement =>
                    {
                        String type = itemElement.Attribute("type")?.Value;

                        var itemType = (EItemTypes)Enum.Parse(typeof(EItemTypes), type, true);
                        CItemFactoryMethod itemFactory = CItemFactoryMethod.GetFactory(itemType);

                        return itemFactory.Create(itemElement);
                    })
                    .ToList();


            var maxHpData = traderData.Element("MaxHp")?.Value ?? throw new Exception("Element 'MaxHp' must by on Monster");
            var maxHp = Int32.Parse(maxHpData);

            var damageData = traderData.Element("Damage")?.Value ?? throw new Exception("Element 'Damage' must by on Monster");
            var damage = Int32.Parse(damageData);


            var trader = new CMonster(maxHp, damage);
            return trader;
        }
    }
}
