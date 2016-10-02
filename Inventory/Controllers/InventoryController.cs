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
            /*
                        InventoryViewModel inventoryVM = new InventoryViewModel()
                        {
                            Inventories = GetInventories(mconn),
                            Items = GetItems(mconn),
                            Locations = GetLocations(mconn),
                            Categories = GetCategories(mconn)
                        };
            */
            return View(results);
        }
    }
}