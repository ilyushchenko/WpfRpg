using System;

namespace Interfaces
{
    public interface IMap
    {
        Int32 Width { get; }
        Int32 Height { get; }
        ICell GetCell(SPoint position);
        Boolean SetUnit(IPositionable unit, SPoint point);
        void RemoveUnit(SPoint position);
    }
}