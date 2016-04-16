using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TheGildedRose.Models
{
    [DataContract]
    public class Transaction
    {
        [DataMember()]
        public User User { get; set; }
        [DataMember()]
        public Item Item { get; set; }

        public Transaction()
        {
            User = new User();
            Item = new Item();
        }
    }
}