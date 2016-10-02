using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.ViewModels
{
    public class InventoryCreate
    {
        public InventoryIndex inventory { get; set; }
        public List<Models.Item> Items { get; set; }
        public List<Models.Location> Locations { get; set; }
        public List<Models.Category> Categories { get; set; }
    }
}