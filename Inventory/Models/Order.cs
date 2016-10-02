using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime NeedByDate { get; set; }
        public bool IsFulfilled { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
    }
}