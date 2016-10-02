using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Inventory.Models;
using MySql.Data.MySqlClient;

namespace Inventory.Controllers
{
    public class BaseController : Controller
    {
        public List<Category> GetCategories(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "select * from Categories";
            MySqlDataReader reader = command.ExecuteReader();
            var result = new List<Category>();
            while (reader.Read())
            {
                result.Add(new Category() { CategoryId = reader.GetInt32(0), CategoryName = reader.GetString(1) });
            }
            reader.Close();
            return result;
        }

        public List<ViewModels.InventoryIndex> GetInventories(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT Inventory.InventoryId, Inventory.ItemId, Inventory.LocationId, "
                                    + "Inventory.Quantity, 	Items.Name AS ItemName,	Locations.Name AS LocationName "
                                    + "FROM Inventory INNER JOIN Items ON Items.ItemId = Inventory.ItemId "
                                    + "INNER JOIN Locations ON Locations.LocationId = Inventory.LocationId";

            MySqlDataReader reader = command.ExecuteReader();
            var inventories = new List<ViewModels.InventoryIndex>();
            while (reader.Read())
            {
                inventories.Add(new ViewModels.InventoryIndex()
                {
                    InventoryId = reader.GetInt32(0),
                    ItemId = reader.GetInt32(1),
                    LocationId = reader.GetInt32(2),
                    Quantity = reader.GetInt32(3),
                    ItemName = reader.GetString(4),
                    LocationName = reader.GetString(5)
                });
            }
            reader.Close();
            return inventories;
        }

        public List<Location> GetLocations(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT * FROM Locations";
            MySqlDataReader reader = command.ExecuteReader();
            var locations = new List<Inventory.Models.Location>();
            while (reader.Read())
            {
                locations.Add(new Inventory.Models.Location()
                {
                    LocationId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2)
                });
            }
            reader.Close();
            return locations;
        }

        public List<Item> GetItems(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT Items.ItemId, Items.CategoryId, Items.Name, Gender, Size, Price, LowLimit, Age from Items";
            MySqlDataReader reader = command.ExecuteReader();
            var result = new List<Item>();
            while (reader.Read())
            {
                result.Add(new Item()
                {
                    ItemId = reader.GetInt32("ItemId"),
                    CategoryId = reader.GetInt32("CategoryId"),
                    Name = reader.GetString("Name"),
                    Gender = reader.GetString("Gender"),
                    Size = reader.GetString("Size"),
                    Price = reader.GetDecimal("Price"),
                    LowLimit = reader.GetInt32("LowLimit"),
                    Age = reader.GetString("Age")
                });
            }
            reader.Close();
            return result;
        }
    }

}