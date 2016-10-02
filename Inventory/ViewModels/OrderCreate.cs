using System.Collections.Generic;
using Inventory.Models;

namespace Inventory.ViewModels
{
    public class OrderCreate
    {
        public Order Order { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public int SelectedDepartmentId { get; set; }
        public int SelectedLocationId { get; set; }
    }
}