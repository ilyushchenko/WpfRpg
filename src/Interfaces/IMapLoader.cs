using System;

namespace Interfaces
{
    public interface IMapLoader
    {
        Int32 GetWidth();
        Int32 GetHeight();
        String GetName();
        ICell GetCell(SPoint position);
        IPositionable GetUnit(SPoint position);
    }
}
