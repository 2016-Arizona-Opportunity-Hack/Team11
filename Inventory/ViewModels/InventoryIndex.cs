using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.ViewModels
{
    public class InventoryIndex
    {
        public int InventoryId { get; set; }
        public int ItemId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string LocationName { get; set; }
        public string ItemGender { get; set; }
        public string ItemSize { get; set; }
        public string ItemAge { get; set; }
        public int ItemLowLimit { get; set; }
    }
}