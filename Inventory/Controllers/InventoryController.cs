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
                                    + "Inventory.Quantity, Items.Name AS ItemName, Locations.Name AS LocationName, Items.Gender AS ItemGender, "
                                    + "Items.Size AS ItemSize, Items.Age AS ItemAge "
                                    + "FROM Inventory INNER JOIN Items ON Items.ItemId = Inventory.ItemId "
                                    + "INNER JOIN Locations ON Locations.LocationId = Inventory.LocationId "
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
                inventory.ItemGender = reader.GetString("ItemGender");
                inventory.ItemSize = reader.GetString("ItemSize");
                inventory.ItemAge = reader.GetString("ItemAge");
                inventory.LocationName = reader.GetString("LocationName");
            }
            reader.Close();
            return inventory;
        }

        private int CheckForDuplicate(ViewModels.InventoryIndex inventory)
        {
            if (inventory == null)
                return 0;

            int rowscount = 0;
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "select count(*) from Inventory where ItemId = "
                    + inventory.ItemId + " AND LocationId = " + inventory.LocationId + ";";

                rowscount = Convert.ToInt32(command.ExecuteScalar());

                if (rowscount > 0)
                    ModelState.AddModelError("Inventory", "An inventory entry for this item and location already exists.");
                
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                if (mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();
                
            }
            return rowscount;
        }

        public ActionResult Edit(int id)
        {
            var inventory = GetInventory(id);
            return View(inventory);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.InventoryIndex inventory)
        {
           // int existingRowCount = CheckForDuplicate(inventory);

            if (!ModelState.IsValid)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('An inventory entry for this item and location already exists.');</script>");

                return View(inventory);
            }

            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "UPDATE Inventory SET Quantity = '" + inventory.Quantity + "' WHERE InventoryId = " + inventory.InventoryId;
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

        public ActionResult Alert()
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();
            var results = GetInventories(mconn);
            var sortedResults = results.OrderBy(c => c.Quantity);
            return View(sortedResults);
        }

        public ActionResult Add()
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();
            var inventoryVM = new ViewModels.InventoryCreate()
            {
                Inventory = new ViewModels.InventoryIndex(),
                Items = GetItems(mconn),
                Locations = GetLocations(mconn),
                Categories = GetCategories(mconn)
            };
            return View(inventoryVM);
        }

        [HttpPost]
        public ActionResult Add(ViewModels.InventoryCreate inventoryVM)
        {
            var rows = CheckForDuplicate(inventoryVM.Inventory);

            if (!ModelState.IsValid || rows > 0)
                return RedirectToAction("Index");

            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();
                var inventory = inventoryVM.Inventory;
                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "INSERT INTO Inventory(ItemId, LocationId, Quantity) VALUES ("
                    + inventory.ItemId + "," + inventory.LocationId + "," + inventory.Quantity + ")";
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
    }
}