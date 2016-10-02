using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Inventory.Models;


namespace Inventory.Controllers
{
    public class InventoryController : BaseController
    {
        public ActionResult Index()
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();
            var results = GetInventories(mconn);
            return View(results);
        }

        private ViewModels.InventoryIndex GetInventory(int id)
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();

            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT Inventory.InventoryId, Inventory.ItemId, Inventory.LocationId, "
                                    + "Inventory.Quantity, 	Items.Name AS ItemName,	Locations.Name AS LocationName "
                                    + "FROM Inventory INNER JOIN Items ON Items.ItemId = Inventory.ItemId "
                                    + "INNER JOIN Locations ON Locations.LocationId = Inventory.LocationId"
                                    + "WHERE InventoryId = " + id;
            MySqlDataReader reader = command.ExecuteReader();
            var inventory = new ViewModels.InventoryIndex();
            while (reader.Read())
            {
                inventory.InventoryId = reader.GetInt32("InventoryId");
                inventory.ItemId = reader.GetInt32("ItemId");
                inventory.LocationId = reader.GetInt32("LocationId");
                inventory.Quantity = reader.GetInt32("Quantity");
                inventory.ItemName = reader.GetString("ItemName");
                inventory.LocationName = reader.GetString("LocationName");
            }
            reader.Close();
            return inventory;
        }

        public ActionResult Edit(int id)
        {
            var inventory = GetInventory(id);
            return View(inventory);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.InventoryIndex inventory)
        {
            //CheckForDuplicate(inventory);

            if (!ModelState.IsValid)
                return View(inventory);

            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "UPDATE Inventories SET Quantity = '" + inventory.Quantity + "' WHERE InventoryId = " + inventory.InventoryId;
                var output = command.ExecuteNonQuery();
                if (output != 1)
                {
                }
            }
            catch (Exception e) { }
            finally
            {
                if (mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            var inventory = new ViewModels.InventoryIndex();
            return View(inventory);
        }

        [HttpPost]
        public ActionResult Add(int test)
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();
            var inventoryVM = new ViewModels.InventoryCreate()
            {
               // Inventory = GetInventory(mconn, id),
                Items = GetItems(mconn),
                Locations = GetLocations(mconn),
                Categories = GetCategories(mconn)
            };
            return View(inventoryVM);
        }
    }
}