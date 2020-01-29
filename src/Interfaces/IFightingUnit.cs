using System;

namespace Interfaces
{
    public interface IFightingUnit : IHealthUnit
    {
        /// <summary>
        /// Attack unit with <see cref="IDamaging"/>
        /// </summary>
        /// <param name="damagingItem">Item which give damage</param>
        /// <returns>Taken damage</returns>
        Int32 Attack(IDamaging damagingItem);
    }
}