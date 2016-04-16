using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheGildedRose.Controllers;
using TheGildedRose.Models;
using TheGildedRose.Interfaces;
using TheGildedRose.Implementations;
using System.Linq;

namespace TheGildedRose.Tests.Controllers
{
    /// <summary>
    /// Summary description for InventoryControllerTest
    /// </summary>
    [TestClass]
    public class InventoryControllerTest
    {
        #region GetInventory_Tests
        [TestMethod]
        public void GetInventory()
        {
            // Arrange
            InventoryController controller = new InventoryController();

            // Act
            Inventory inventory = controller.GetInventory();

            // Assert - Inventory not null and status code successful.
            Assert.IsNotNull(inventory);
            Assert.AreEqual(0, inventory.Status.code);
        }
        #endregion

        #region BuyItem_Tests_ItemRelated
        [TestMethod]
        public void BuyItem_ItemNull()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = null;

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - Data Anomaly Response
            Assert.IsNotNull(purchase);
            Assert.AreEqual(9001, purchase.code);
        }
        [TestMethod]
        public void BuyItem_ItemEmpty()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = new Item();

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - Item Not Found Response
            Assert.IsNotNull(purchase);
            Assert.AreEqual(1, purchase.code);
        }
        [TestMethod]
        public void BuyItem_NegativeID()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = new Item(-1,7);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - Data Anomaly Response
            Assert.IsNotNull(purchase);
            Assert.AreEqual(9001, purchase.code);
        }
        [TestMethod]
        public void BuyItem_IDNotInStock()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = new Item(100,7);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - Item Not Found
            Assert.IsNotNull(purchase);
            Assert.AreEqual(1, purchase.code);
        }
        [TestMethod]
        public void BuyItem_QunatityUnavailable()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = new Item(1, 999999);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - Not Enough Quantity Response.
            Assert.IsNotNull(purchase);
            Assert.AreEqual(2, purchase.code);
        }
        [TestMethod]
        public void BuyItem_NegativeQuantity()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = new Item(1, -99);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - Data Anomaly Response
            Assert.IsNotNull(purchase);
            Assert.AreEqual(9001, purchase.code);
        }
        [TestMethod]
        public void BuyItem_QuantityUpdate()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            transaction.User.Username = "jedidjabourgeois";
            transaction.User.APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191";
            transaction.Item = new Item(1, 3);

            // Act
            Item purchasedItem = controller.GetInventory().Items.FirstOrDefault(i => i.ID == transaction.Item.ID);
            int originalQuantity = purchasedItem.Quantity;
            Status purchase = controller.BuyItem(transaction);

            // Assert - Successful Response and Quantity Updated Properly
            Assert.IsNotNull(purchase);
            Assert.AreEqual(0, purchase.code);

            //Assert - Verify Quantity Updated
            Assert.IsNotNull(purchasedItem);
            Assert.AreEqual(originalQuantity - transaction.Item.Quantity,
                purchasedItem.Quantity);

        }
        #endregion

        #region BuyItem_Tests_UserRelated
        [TestMethod]
        public void BuyItem_UserUnauthorized_EmptyUser()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();

            transaction.Item = new Item(1, 3);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - 401 Response Expected.
            Assert.IsNotNull(purchase);
            Assert.AreEqual(401, purchase.code);
        }
        [TestMethod]
        public void BuyItem_UserUnauthorized_TokenExpired()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();
            
            transaction.User.Username = "tonyawad";
            transaction.User.APIAccessToken = "f87b72c4d44b434af8e2bb1486000fe2b89865172d51263c080e167b002473df";

            transaction.Item = new Item(1, 3);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - 401 Response Expected.
            Assert.IsNotNull(purchase);
            Assert.AreEqual(401, purchase.code);

        }
        [TestMethod]
        public void BuyItem_UserUnauthorized_InvalidUser()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();

            transaction.User.Username = "kenny1234";
            transaction.User.APIAccessToken = "f87b72c4d44b434af8e2bb1486000fe2b89865172d51263c080e167b002473df";

            transaction.Item = new Item(1, 3);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - 401 Response Expected.
            Assert.IsNotNull(purchase);
            Assert.AreEqual(401, purchase.code);

        }
        [TestMethod]
        public void BuyItem_UserUnauthorized_InvalidToken()
        {
            // Arrange
            InventoryController controller = new InventoryController();
            Transaction transaction = new Transaction();

            transaction.User.Username = "kennykolstad";
            transaction.User.APIAccessToken = "asdasdasdadasdsad";

            transaction.Item = new Item(1, 3);

            // Act
            Status purchase = controller.BuyItem(transaction);

            // Assert - 401 Response Expected.
            Assert.IsNotNull(purchase);
            Assert.AreEqual(401, purchase.code);
        }
        #endregion


    }
}
