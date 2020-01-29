using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ITradable
    {
        Int32 Gold { get; set; }
        List<IInventoryItem> Inventory { get; }
    }
}