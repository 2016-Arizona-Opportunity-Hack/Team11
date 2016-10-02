using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Inventory.Models;
using Inventory.ViewModels;

namespace Inventory.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var strConnString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

            MySqlConnection mconn = new MySqlConnection(strConnString);

            var result = new List<OrderIndex>();

            try {
                mconn.Open();

                MySqlCommand command = mconn.CreateCommand();
                command.CommandText = "Select O.OrderId, O.DepartmentId, O.LocationId, O.IsFulfilled, O.NeedByDate, L.Name AS LocationName, D.Name AS DepartmentName From Orders O " + 
                                         "Left Join Locations L on (O.LocationId = L.LocationId) " +
                                         "Left Join Departments D on (O.DepartmentId = D.DepartmentId)";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   var order = new Order
                    {
                        OrderId = reader.GetInt32("OrderId"),
                        DepartmentId = reader.GetInt32("DepartmentId"),
                        LocationId = reader.GetInt32("LocationId"),
                        IsFulfilled = reader.GetBoolean("IsFulfilled"),
                        NeedByDate = Convert.ToDateTime(reader.GetMySqlDateTime("NeedByDate"))
                    };

                    result.Add(new OrderIndex()
                    {  Order = order,
                       LocationName = reader.GetString("LocationName"),
                       DepartmentName = reader.GetString("DepartmentName")
                    });
                }
                reader.Close();
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();
            }
            return View(result);
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