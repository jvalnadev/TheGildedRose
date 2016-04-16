using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGildedRose.Models;

namespace TheGildedRose.Interfaces
{
    /// <summary>
    /// ----------------------------------------------------------------------------------------------------------
    /// "IInventory"        Inventory Interface. Provides access to two functions.
    /// ----------------------------------------------------------------------------------------------------------
    /// </summary>
    public interface IInventory
    {
        Inventory GetInventory();
        Status BuyItem(Transaction item);
    }
}
