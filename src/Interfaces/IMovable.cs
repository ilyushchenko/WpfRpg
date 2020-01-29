using System;

namespace Interfaces
{
    public interface IMovable : IPositionable
    {
        Int32 MovingEnergy { get; set; }
        Int32 MaxMovingEnergy { get; set; }
        Boolean CanMove();
    }
}