using System;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Terrains
{
    [DataContract]
    public class CDirt : ITerrain
    {
        public CDirt()
        {
            MovementPenalty = 2;
        }

        public String Name => @"Dirt";

        [DataMember]
        public Int32 MovementPenalty { get; set; }
    }
}