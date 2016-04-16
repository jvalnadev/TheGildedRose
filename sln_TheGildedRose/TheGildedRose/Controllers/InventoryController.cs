using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheGildedRose.Implementations;
using TheGildedRose.Interfaces;
using TheGildedRose.Models;

namespace TheGildedRose.Controllers
{
    public class InventoryController : ApiController
    {

        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// HTTP Method: GET
        /// Action Name: getinventory
        /// ----------------------------------------------------------------------------------------------------------
        /// "GetInventory"      function This method takes no input parameters and returns the list of items 
        ///                     we have in the inventory.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        [HttpGet()]
        [ActionName("getinventory")]
        public Inventory GetInventory(){

            //1. Instantiate Interface and call GetInventory function.
            IInventory inventory = new InventoryImpl();
            return inventory.GetInventory();
        }

        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// HTTP Method: POST
        /// Action Name: buyitem
        /// ----------------------------------------------------------------------------------------------------------
        /// "BuyItem"           This method takes an input parameter as a transcation object which contains a user 
        ///                     object and an item object. We first verify if the user is authenicated and then proceed 
        ///                     to execute the transaction.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        [HttpPost()]
        [ActionName("buyitem")]
        public Status BuyItem([FromBody()] Transaction transaction)
        {
            //1. Instantiate Interface and call BuyItem function.
            IInventory inventory = new InventoryImpl();
            return inventory.BuyItem(transaction);
        }


        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "IList<Item>"       A list of items to simulate existing items on the database. 
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// </summary>
        public static IList<Item> items = new List<Item>()
        {
            new Item { ID = 1, Name= "MacBook Air",
                        Description = "Whatever the task, new fifth-generation Intel Core i5 and i7 processors with Intel HD Graphics 6000 are up to it.",
                        Price = 1099, Quantity = 10 },
            new Item { ID = 2, Name= "Burton Custom Mystery Snowboard",
                        Description = "The absolute lightest weight science in the entire Burton line applied to the Custom’s versatile and venerated shape. Simply genius.",
                        Price = 500, Quantity = 0 },
            new Item { ID = 3, Name= "GoPro Hero 4",
                        Description = "HERO4 Session is the most wearable and mountable GoPro ever.",
                        Price = 559, Quantity = 100 }
        };

    }
}
