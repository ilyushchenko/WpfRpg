using System;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Items
{
 
    [DataContract]
    public class CWeapon : CItemBase, IWeapon
    {
        private CWeapon(Guid id, String name, Int32 cost, Int32 damage) : base(id, name, cost)
        {
            Damage = damage;
        }

        [DataMember]
        public Int32 Damage { get; set; }

        public static CWeapon Create(Guid id, String name, Int32 cost, Int32 damage)
        {
            return new CWeapon(id, name, cost, damage);
        }
    }
}