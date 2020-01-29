using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Heroes;
using Core.Models.Items;

namespace Core.Models.Items
{
    public class CHealthPotion : CSingleUseItem
    {
        private const String HealthPotionId = "4593C0BC-B8CB-4CB0-AA6F-95EC1DCD57C4";
        public CHealthPotion() : base(Guid.Parse(HealthPotionId), "Small health potion", 100, 0, "Instant restore 10 HP")
        {
        }

        public CHealthPotion(Guid id, String name, Int32 cost, Double weight, String description) :
            base(id, name, cost, weight, description)
        {
        }

        private const Int32 HealthCount = 10;
        
        public override Boolean ApplyItem(CHeroBase hero)
        {
            hero.HealthPoints += HealthCount;
            return true;
        }
    }
}
