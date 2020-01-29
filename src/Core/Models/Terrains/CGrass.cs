using System;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Terrains
{
    [DataContract]
    public class CGrass : ITerrain
    {
        public CGrass()
        {
            MovementPenalty = 1;
        }
        public String Name => @"Grass";

        [DataMember]
        public Int32 MovementPenalty { get; set; }
    }
}