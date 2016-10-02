using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class InventoryViewModel
    {
        public List<Inventory> Inventories { get; set; }
        public List<Item> Items { get; set; }
        public List<Location> Locations { get; set; }
        public List<Category> Categories { get; set; }
    }
}