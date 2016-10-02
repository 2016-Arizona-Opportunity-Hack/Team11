using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        
//        public ActionResult Create()
//        {
//            var category = new Category
//            {
//                IsEnabled = true
//            };
//
//            return View(category);
//        }

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