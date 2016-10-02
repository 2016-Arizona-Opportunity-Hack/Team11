using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Item
    {
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public string Age { get; set; }
        public int LowLimit { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; 
        }
    }
}