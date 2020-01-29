using System;
using Interfaces;

namespace MainService.Models
{
    public class ItemTransferModel
    {
        public ItemTransferModel()
        {
        }

        public ItemTransferModel(IInventoryItem item)
        {
            Item = item;
            Type = Item.GetType();
        }

        public Type Type { get; set; }
        public IInventoryItem Item { get; set; }
    }
}