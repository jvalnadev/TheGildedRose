using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using TheGildedRose.Interfaces;
using TheGildedRose.Models;
using TheGildedRose.Controllers;

namespace TheGildedRose.Implementations
{
    public class InventoryImpl : IInventory
    {
        

        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "GetInventory"      Retrieves the list of inventory and returns it as an Inventory class. 
        ///                     Inventory class contains a "Status" object and a IList of Items. 
        ///                     If an exception occurs, we are going to return an error status object and an empty IList. 
        ///                     We are also going to log it to the event viewer logs.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        public Inventory GetInventory()
        {
            //0. Variable Initialization
            Inventory inventory = new Inventory();

            //1. Retrieve Data
            try
            {
                inventory.Items = InventoryController.items;
                inventory.Status = new Status(0, "Successful");
            }
            catch (Exception ex)
            {
                //yy. If an exception occurs, log it in the system logs. Also generate an error status.
                Logging.Logger.WriteErrorMessage("GetInventory General Exception. Message: " + ex.Message);
                inventory.Status = new Status(9000, "System is currently unavailable. Please try again later.");
            }

            //zz. Return Items
            return inventory;
        }

        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "BuyItem"           Execute a purchase transaction. It first verifies if the user is authenicated, checks 
        ///                     for data anomalies, makes sure the item exists and has quantity to be purchased is in 
        ///                     stock. When all above tests are passed, transaction is committed and the quantity is 
        ///                     decremented. If an exception occurs, we log it to the system logs and respond with an
        ///                     error status.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="transaction"></param>
        public Status BuyItem(Transaction transaction)
        {
            //1. Variable Initialization
            Status status = new Status();

            //2. Buy Item
            try
            {
                //1. Authenicate User
                IUserAuthenication userAuthenication = new UserAuthenicationImpl();
                bool authenicated = userAuthenication.AuthenicateUser(transaction.User);

                //2. Check if user is authenicated
                if (!authenicated) return new Status(401,"Unauthorized to access API. Please contact us at +1-123-444-5678 for assistance.");

                //3. Look for data anomalies
                if (transaction.Item == null || transaction.Item.ID < 0 || transaction.Item.Quantity < 0)
                    return new Status(9001, "Data anomaly detected. Please verify your item object and re-transmit");
                
                //4. Search for an item then evaluate search result.
                Item searchItem = GetInventory().Items.FirstOrDefault(i => i.ID == transaction.Item.ID);
                if (searchItem == null) return new Status(1, "Item not found");

                //5. Verify that the quantity we are purchasing is available.
                if(searchItem.Quantity < transaction.Item.Quantity)
                { 
                    return new Status(2, "Not enough quantity in stock");
                }

                //5. Commit Transaction by decrementing quantity
                searchItem.Quantity -= transaction.Item.Quantity;

                //6. Finally return status.
                status = new Status(0, "Purchase Successful");
                
            }
            catch (Exception ex)
            {
                //y1. If an exception occurs, log it in the system logs.
                Logging.Logger.WriteErrorMessage("BuyItem General Exception. Message: " + ex.Message);

                //y2. Formulate Error Status
                status = new Status(9999,
                    "System currently unavailable, please try again later.");
            }

            //zz. Return Status
            return status;
        }

    }
}