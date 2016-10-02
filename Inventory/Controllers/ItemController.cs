using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Inventory.Models;
using MySql.Data.MySqlClient;

namespace Inventory.Controllers
{
    public class ItemController : BaseController
    {

        // GET: /Category/
        public ActionResult Index()
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();

            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "select * from Items";
            //MySqlDataReader reader = command.ExecuteReader();
            var result = GetItems(mconn);

            //reader.Close();
            return View(result);
        }

        private Item GetItem(int id)
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();

            var item = new Item();
            item.Categories = GetCategories(mconn);

            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "select * from Items where ItemId = " + id;
            MySqlDataReader reader = command.ExecuteReader();
                   
            while (reader.Read())
            {
                item.CategoryId = reader.GetInt32("CategoryId");
                item.ItemId = reader.GetInt32("ItemId");
                item.Name = reader.GetString("Name");
                item.Gender = reader.GetString("Gender");
                item.Size = reader.GetString("Size");
                item.Age = reader.GetString("Age");
                item.LowLimit = reader.GetInt32("LowLimit");
                item.Price = reader.GetDecimal("Price");
            }

            reader.Close();
            return item;
        }

        public ActionResult Create()
        {
            var newItem = new Item();
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();
            newItem.Categories = GetCategories(mconn);
            newItem.IsDeleted = 0;
            return View(newItem);
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            CheckForDuplicate(item);

            if (!ModelState.IsValid)
                return View(item);
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();
                String outi = "Hi " + item.Name + item.CategoryId;
                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "INSERT INTO Items (CategoryId, Name, Price, "
                                    + "LowLimit, Gender, Age, Size, ModifiedBy, "
                                    + "Timestamp, IsDeleted) "
                                    + "values ("+item.CategoryId+", "+item.Name+", "+item.Price
                                    + ", "+item.LowLimit+", "+item.Gender+"', '"+item.Age+"' , '"+item.Size
                                    + "', "+item.ModifiedBy+", "+item.Timestamp+", "+item.IsDeleted+");"; 
                var output=command.ExecuteNonQuery();
                if (output != 1) {
                    Console.Out.Write("Fail to write item");
                }
            }
            catch (Exception e) {               
                Console.Out.Write("Fail to write item");               
            }
            finally {
                if (mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();
            }

                return RedirectToAction("Index");
        }

        private void CheckForDuplicate(Item item)
        {            
                if (item == null)
                    return;

                var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

                MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "select count(*) from Items where Name = " + item.Name 
                    + " AND CategoryId = " +item.CategoryId + " AND Age = "+item.Age+" AND "
                    + "Size = " + item.Size + " AND Gender = "+item.Gender+")";

               int rowscount = Convert.ToInt32(command.ExecuteScalar());
               
                if (rowscount > 0)
                    ModelState.AddModelError("Item ID", "An item with this name already exists.");
            }
            catch (Exception e)
            {
                
            }
            finally {
                if(mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();   
            }
        }

        public ActionResult Edit(int id)
        {
            var item = GetItem(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            CheckForDuplicate(item);

            if (!ModelState.IsValid)
                return View(item);

           var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "Update Items Set Name = '" + item.Name + "', CategoryId = " + item.CategoryId + ", "
                                     +"Price = " + item.Price + ", LowLimit = " + item.LowLimit +", "
                                     +"Age = '" + item.Age + "', Gender = '" + item.Gender + "', Size = '" + item.Size + "' "
                                    + " Where ItemId = " + item.ItemId;
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