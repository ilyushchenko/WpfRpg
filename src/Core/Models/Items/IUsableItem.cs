using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Heroes;
using Interfaces;

namespace Core.Models.Items
{
    public interface IUsableItem : IInventoryItem
    {
        Boolean CanUse(CHeroBase hero);
        void Use(CHeroBase hero);
        Boolean CanUseAgain();
    }
}
