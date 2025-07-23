using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }

    }
}
