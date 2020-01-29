using System;
using System.Runtime.Serialization;
using Core.Models.Heroes;

namespace Core.Models.Items
{
    [DataContract]
    public abstract class CSingleUseItem : CItemBase, IUsableItem
    {
        protected CSingleUseItem(Guid id,  String name, Int32 cost, Double weight, String description) : 
            base(id, name, cost, description: description)
        {
            Name = name;
            Cost = cost;
            Weight = weight;
            Description = description;
        }

        [DataMember]
        private Boolean _isUsed;

        public Boolean CanUse(CHeroBase hero)
        {
            return !_isUsed;
        }

        public void Use(CHeroBase hero)
        {
            if (ApplyItem(hero))
            {
                _isUsed = true;
            }
        }

        public abstract Boolean ApplyItem(CHeroBase hero);


        public Boolean CanUseAgain()
        {
            return !_isUsed;
        }
    }
}