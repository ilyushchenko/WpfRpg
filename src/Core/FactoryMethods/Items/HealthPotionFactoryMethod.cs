using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Core.Models.Items;
using Interfaces;
using Newtonsoft.Json;

namespace Core.FactoryMethods.Items
{
    public class CHealthPotionFactoryMethod : CItemFactoryMethod
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

            String description = itemData.Attribute("description")?.Value;

            var healthPotion = new CHealthPotion(id, name, cost, 0, description);
            return healthPotion;
        }

        public override IInventoryItem CreateFromJson(String itemJsonData)
        {
            return JsonConvert.DeserializeObject<CHealthPotion>(itemJsonData);
        }
    }
}