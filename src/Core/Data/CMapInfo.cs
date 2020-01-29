using System;
using System.Runtime.Serialization;

namespace Core.Data
{
    [DataContract]
    public class CMapInfo
    {
        private CMapInfo(Guid id, String name, Int32 width, Int32 height)
        {
            Id = id;
            Name = name;
            Width = width;
            Height = height;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public Int32 Width { get; set; }

        [DataMember]
        public Int32 Height { get; set; }

        public static CMapInfo Create(Guid id, String name, Int32 width, Int32 height)
        {
            return new CMapInfo(id, name, width, height);
        }
    }
}
