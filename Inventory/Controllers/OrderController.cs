using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Inventory.Models;
using Inventory.ViewModels;

namespace Inventory.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Index()
        {
            var mconn = CreateConnection();

            var result = new List<OrderIndex>();

            try
            {
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


        public ActionResult Create()
        {
            var NewCreateOrder = new OrderCreate(){ Order = new Order { IsFulfilled = false } };

            var mconn = CreateConnection();

            try
            {
                mconn.Open();
                NewCreateOrder.Departments = GetDepartments(mconn);
                NewCreateOrder.Locations = GetLocations(mconn);    
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (mconn.State == System.Data.ConnectionState.Open)
                    mconn.Close();
            }

            return View(NewCreateOrder);
        }
    }
}