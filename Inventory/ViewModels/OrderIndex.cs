using System.Collections.Generic;
using Inventory.Models;

namespace Inventory.ViewModels
{
    public class OrderIndex
    {
        public Order Order { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
    }
}