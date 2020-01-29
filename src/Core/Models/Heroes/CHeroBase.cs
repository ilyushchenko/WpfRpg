using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Models.Items;
using Interfaces;

namespace Core.Models.Heroes
{
    [DataContract]
    public class CHeroBase : IHero, IMovable, IFightingUnit, ITradable, IRoundUpdatable
    {
        [DataMember]
        private IMap _map;

        [DataMember]
        private Int32 _hp;

        public CHeroBase(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
        {
            Name = name;
            Inventory = new List<IInventoryItem>();
            MaxHealthPoints = maxHealthPoints;
            HealthPoints = maxHealthPoints;

            MaxMovingEnergy = maxMovingEnergy;
            MovingEnergy = maxMovingEnergy;

            MaxWeight = maxWeight;

            Gold = 1000;
            NextLevelXp = 1000;
        }

        [DataMember]
        public String Name { get; set; }

        public Int32 HealthPoints
        {
            get => _hp;
            set
            {
                _hp += value;
                if (_hp > MaxHealthPoints)
                {
                    _hp = MaxHealthPoints;
                }
            }
        }

        [DataMember]
        public Int32 MaxHealthPoints { get; set; }

        [DataMember]
        public Int32 Level { get; set; }

        [DataMember]
        public Int32 Xp { get; set; }

        [DataMember]
        public Int32 NextLevelXp { get; set; }

        [DataMember]
        public List<IInventoryItem> Inventory { get; set; }

        [DataMember]
        public Int32 Gold { get; set; }

        [DataMember]
        public Double MaxWeight { get; set; }

        [DataMember]
        public Int32 MovingEnergy { get; set; }

        [DataMember]
        public Int32 MaxMovingEnergy { get; set; }

        [DataMember]
        public SPoint Position { get; private set; }

        public Double CurrentWeight => Inventory.Sum(i => i.Weight);

        public event EventHandler Updated;

        public virtual Boolean CanMove()
        {
            return CurrentWeight <= MaxWeight;
        }

        public Boolean UseItem(IUsableItem item)
        {
            if (!item.CanUse(this))
            {
                return false;
            }

            item.Use(this);
            Notify();
            return true;
        }

        public void AddXp(Int32 xp)
        {
            Xp += xp;
            while (Xp >= NextLevelXp)
            {
                Level++;
                Xp -= NextLevelXp;
            }
            Notify();
        }

        public void SetMap(IMap map)
        {
            _map = map;
        }

        public void SetPosition(SPoint newPosition)
        {
            Position = newPosition;
            _map.SetUnit(this, newPosition);
            Notify();
        }

        public virtual Int32 Attack(IDamaging damagingItem)
        {
            HealthPoints -= damagingItem.Damage;
            Notify();
            return damagingItem.Damage;
        }

        public void RestoreAfterRound()
        {
            MovingEnergy = MaxMovingEnergy;
        }

        public void AddToInventory(IEnumerable<IInventoryItem> items)
        {
            Inventory.AddRange(items);
            Notify();
        }

        public void AddToInventory(IInventoryItem item)
        {
            Inventory.Add(item);
            Notify();
        }

        private void Notify()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}