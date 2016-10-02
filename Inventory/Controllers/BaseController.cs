using System.Collections.Generic;
using System.Web.Mvc;
using Inventory.Models;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Inventory.Controllers
{
    public class BaseController : Controller
    {
        public MySqlConnection CreateConnection()
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            return mconn;
        }

        public List<Category> GetCategories(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "Select * from Categories";
            MySqlDataReader reader = command.ExecuteReader();
            var result = new List<Category>();
            while (reader.Read())
            {
                result.Add(new Category() { CategoryId = reader.GetInt32("CategoryId"), CategoryName = reader.GetString("CategoryName") });
            }
            reader.Close();
            return result;
        }

        public List<ViewModels.InventoryIndex> GetInventories(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT Inventory.InventoryId, Inventory.ItemId, Inventory.LocationId, "
                                    + "Inventory.Quantity, 	Items.Name AS ItemName,	Locations.Name AS LocationName, "
                                    + "Items.Gender AS ItemGender, Items.Size AS ItemSize, Items.Age AS ItemAge "
                                    + "FROM Inventory INNER JOIN Items ON Items.ItemId = Inventory.ItemId "
                                    + "INNER JOIN Locations ON Locations.LocationId = Inventory.LocationId";

            MySqlDataReader reader = command.ExecuteReader();
            var inventories = new List<ViewModels.InventoryIndex>();
            while (reader.Read())
            {
                inventories.Add(new ViewModels.InventoryIndex()
                {
                    InventoryId = reader.GetInt32("InventoryId"),
                    ItemId = reader.GetInt32("ItemId"),
                    LocationId = reader.GetInt32("LocationId"),
                    Quantity = reader.GetInt32("Quantity"),
                    ItemName = reader.GetString("ItemName"),
                    ItemGender = reader.GetString("ItemGender"),
                    ItemSize = reader.GetString("ItemSize"),
                    ItemAge = reader.GetString("ItemAge"),
                    LocationName = reader.GetString("LocationName")
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
            var locations = new List<Location>();
            while (reader.Read())
            {
                locations.Add(new Location()
                {
                    LocationId = reader.GetInt32("LocationId"),
                    Name = reader.GetString("Name"),
                    Address = reader.GetString("Address")
                });
            }
            reader.Close();
            return locations;
        }

        public List<Department> GetDepartments(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT * FROM Departments";
            MySqlDataReader reader = command.ExecuteReader();
            var departments = new List<Department>();
            while (reader.Read())
            {
                departments.Add(new Department()
                {
                    DepartmentId = reader.GetInt32("DepartmentId"),
                    Name = reader.GetString("Name")
                });
            }
            reader.Close();
            return departments;
        }

        public List<Item> GetItems(MySqlConnection mconn)
        {
            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "SELECT Items.CategoryId, Items.ItemId, Items.Price, Items.Gender, Items.Size, Items.LowLimit, Items.Age, Items.Name, Categories.CategoryId AS catID, Categories.CategoryName"
                                   +" FROM Items INNER JOIN Categories ON Items.CategoryId = Categories.CategoryId";
            MySqlDataReader reader = command.ExecuteReader();
            var result = new List<Item>();
            while (reader.Read())
            {
                result.Add(new Item()
                {
                    ItemId = reader.GetInt32("ItemId"),
                    CategoryId = reader.GetInt32("CategoryId"),
                    CategoryName = reader.GetString("CategoryName"),
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