using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGildedRose.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Item() {}
        public Item(int ID, int Quantity)
        {
            this.ID = ID;
            this.Quantity = Quantity;
        }
        public Item(int ID, string Name, string Description, int Price, int Quantity)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Quantity = Quantity;
        }


    }
}