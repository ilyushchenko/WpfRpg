using System;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Items
{
    [DataContract]
    public abstract class CItemBase : IInventoryItem
    {
        protected CItemBase(Guid id, String name, Int32 cost, Double weight = 0, String description = null)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Weight = weight;
            Description = description;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public Int32 Cost { get; set; }

        [DataMember]
        public Double Weight { get; set; }

        [DataMember]
        public String Description { get; set; }
    }
}