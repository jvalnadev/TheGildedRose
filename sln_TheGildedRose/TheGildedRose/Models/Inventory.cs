using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGildedRose.Models
{
    public class Inventory
    {
        public Status Status { get; set; }
        public IList<Item> Items { get; set; }
    }
}