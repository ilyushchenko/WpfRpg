using System;

namespace Interfaces
{
    public interface IInventoryItem
    {
        String Name { get; }
        Int32 Cost { get; set; }
        Double Weight { get; }
        String Description { get; }
    }
}