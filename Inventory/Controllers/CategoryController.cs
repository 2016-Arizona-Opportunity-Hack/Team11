using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
                result.Add(new Category() { Id = reader.GetInt32(0), CategoryName=reader.GetString(1) });
                //reader["column_name"].ToString()
            }

            reader.Close();
            return View(result);
         //   return View();
        }
        public ActionResult Create() {
            var newcat = new Category();

            return View(newcat);
    }
    
    
}