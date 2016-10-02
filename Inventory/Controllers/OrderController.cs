using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace Inventory.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

      
        public String Create()
        {
            string strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;
            //string id, name;
            MySqlConnection mconn = new MySqlConnection(strConnString);
            mconn.Open();

            MySqlCommand command = mconn.CreateCommand();
            command.CommandText = "select * from Categories";
            MySqlDataReader reader = command.ExecuteReader();
            String result=null;
            while (reader.Read())
            {
                result = result + reader.GetString(0);
                //reader["column_name"].ToString()
            }
            
            reader.Close();
            return result;
        }


              
    

//        [HttpPost]
//        public ActionResult Create([Bind(Include = "Name,IsEnabled,SequenceNumber,Description")] Category category)
//        {
//            //if (!ModelState.IsValid)
//            //    return View(category);
//
//            //category.Name = category.Name.NullTrim();
//            //category.Description = category.Description.NullTrim();
//
//            //_db.Categories.Add(category);
//            //_db.SaveChanges();
//
//            //return RedirectToAction("Index");
//        }
    }
}