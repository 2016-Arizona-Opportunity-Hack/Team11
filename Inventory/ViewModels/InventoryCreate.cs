using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.ViewModels
{
    public class InventoryCreate
    {
        public InventoryIndex Inventory { get; set; }
        public List<Models.Item> Items { get; set; }
        public List<Models.Location> Locations { get; set; }
        public List<Models.Category> Categories { get; set; }
        public int SelectedItemId { get; set; }
        public int SelectedLocationId { get; set; }
    }
}