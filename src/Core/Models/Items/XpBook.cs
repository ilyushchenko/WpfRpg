using System;
using Core.Models.Heroes;

namespace Core.Models.Items
{
    public class CXpBook : CSingleUseItem
    {
        private const String XpBookId = "085F3034-A378-4B3B-A5C6-00EA8190CA6B";
        public CXpBook() : base(Guid.Parse(XpBookId), "XP Book", 200, 0, $"Give to hero {XpCount} XP")
        {
        }

        public CXpBook(Guid id, String name, Int32 cost, Double weight, String description) :
            base(id, name, cost, weight, description)
        {
        }

        private const Int32 XpCount = 500;

        public override Boolean ApplyItem(CHeroBase hero)
        {
            hero.AddXp(XpCount);
            return true;
        }
    }
}