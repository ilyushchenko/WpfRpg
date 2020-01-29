using System;

namespace Interfaces
{
    public interface ITerrain
    {
        String Name { get; }
        Int32 MovementPenalty { get; set; }
    }
}