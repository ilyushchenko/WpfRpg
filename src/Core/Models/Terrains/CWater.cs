using System;
using System.Runtime.Serialization;
using Interfaces;

namespace Core.Models.Terrains
{
    [DataContract]
    public class CWater : ITerrain
    {
        public CWater()
        {
            MovementPenalty = 3;
        }

        public String Name => @"Water";

        [DataMember]
        public Int32 MovementPenalty { get; set; }
    }
}