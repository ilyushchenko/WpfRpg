using System;
using System.Xml.Linq;
using Core.Models.Items;
using Interfaces;
using Newtonsoft.Json;

namespace Core.FactoryMethods.Items
{
    public class CWeaponFactoryMethod : CItemFactoryMethod
    {
        public override IInventoryItem Create(XElement itemData)
        {
            String idData = itemData.Attribute("id")?.Value ??
                              throw new Exception($"Attribute 'id' not found in {itemData.Value}");
            Guid id = Guid.Parse(idData);
            String name = itemData.Attribute("name")?.Value ??
                          throw new Exception($"Attribute 'name' not found in {itemData.Value}");
            String costData = itemData.Attribute("cost")?.Value ??
                              throw new Exception($"Attribute 'cost' not found in {itemData.Value}");
            Int32 cost = Int32.Parse(costData);
            String damageData = itemData.Element("damage")?.Value ??
                                throw new Exception($"Element 'damage' not found in {itemData.Value}");
            Int32 damage = Int32.Parse(damageData);
            CWeapon weapon = CWeapon.Create(id, name, cost, damage);
            return weapon;
        }

        public override IInventoryItem CreateFromJson(String itemJsonData)
        {
            return JsonConvert.DeserializeObject<CWeapon>(itemJsonData);
        }
    }
}