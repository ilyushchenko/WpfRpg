using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ITrader
    {
        IReadOnlyList<IInventoryItem> Inventory { get; }
        Int32 Gold { get; set; }
        Boolean BuyItem(ITradable seller, IInventoryItem item);
        Boolean SellItem(ITradable buyer, IInventoryItem item);
    }
}