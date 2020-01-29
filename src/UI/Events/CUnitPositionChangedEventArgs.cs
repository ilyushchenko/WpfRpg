using System;
using Interfaces;

namespace UI.Events
{
    public class CUnitPositionChangedEventArgs : EventArgs
    {
        public CUnitPositionChangedEventArgs(IMovable unit, ICell oldCell, ICell newCell)
        {
            Unit = unit;
            OldCell = oldCell;
            NewCell = newCell;
        }

        public IMovable Unit { get; }
        public ICell OldCell { get; }
        public ICell NewCell { get; }
    }
}