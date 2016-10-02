using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Inventory.Models;
using MySql.Data.MySqlClient;

namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {

        // GET: /Category/
        public ActionResult Index()
        {
            //var vm = new Models.Category();
            //IEnumerable<Category> clist = new List<Category>();
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();

            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "select * from Categories";
            MySqlDataReader reader = command.ExecuteReader();
            var result = new List<Category>();
            while (reader.Read())
            {
                result.Add(new Category() { CategoryId = reader.GetInt32(0), CategoryName = reader.GetString(1) });
                //reader["column_name"].ToString()
            }

            reader.Close();
            return View(result);
            //   return View();
        }

        public ActionResult Create()
        {
            var newcat = new Category();
            return View(newcat);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            CheckForDuplicate(category);

            if (!ModelState.IsValid)
                return View(category);
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "Insert into Categories (CategoryName) values ('"+category.CategoryName+"')";
                var output=command.ExecuteNonQuery();
                if (output != 1) {
                }
            }
            catch (Exception e) { }
            finally {
                if (mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();
            }

                return RedirectToAction("Index");
        }

        private void CheckForDuplicate(Category category)
        {
            
                if (category == null)
                    return;

                var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

                MySqlConnection mconn = new MySqlConnection(strConnString);
            try
            {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "select count(*) from Categories where categoryId = " + category.CategoryId;

                //MySqlDataReader reader = command.ExecuteReader();

                int rowscount = Convert.ToInt32(command.ExecuteScalar());
                

                if (rowscount > 0)
                    ModelState.AddModelError("Category Name", "A Category with this name already exists.");
            }
            catch (Exception e)
            {
                
            }
            finally {
                if(mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();   
            }
        }
    }

}