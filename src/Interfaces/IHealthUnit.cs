using System;

namespace Interfaces
{
    public interface IHealthUnit
    {
        Int32 HealthPoints { get; }
        Int32 MaxHealthPoints { get; }
    }
}