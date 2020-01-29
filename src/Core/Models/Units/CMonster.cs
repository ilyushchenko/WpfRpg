using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Units
{
    [DataContract]
    public class CMonster : IPositionable, IFightingUnit
    {
        [DataMember]
        private IMap _map;

        public CMonster(Int32 maxHealthPoints, Int32 damage)
        {
            MaxHealthPoints = maxHealthPoints;
            HealthPoints = MaxHealthPoints;
            Damage = damage;
            Loot = new List<IInventoryItem>();
        }

        [DataMember]
        public List<IInventoryItem> Loot { get; }
        [DataMember]
        public Int32 Damage { get; set; }
        [DataMember]
        public Int32 HealthPoints { get; set; }
        [DataMember]
        public Int32 MaxHealthPoints { get; set; }
        [DataMember]
        public SPoint Position { get; private set; }

        public Int32 Attack(IDamaging damagingItem)
        {
            HealthPoints -= damagingItem.Damage;
            if (HealthPoints <= 0) Die();
            return damagingItem.Damage;
        }

        public void SetMap(IMap map)
        {
            _map = map;
        }

        public void SetPosition(SPoint position)
        {
            Position = position;
            _map.SetUnit(this, position);
        }

        private void Die()
        {
            _map.RemoveUnit(Position);
        }
    }
}