using System.Collections.Generic;

namespace LojaAppApi.Model
{
    public class Order
    {
        public int OrderID { get; set; }

        public int OrderNo { get; set; }

        public int CustomerID { get; set; }

        public string PMethod { get; set; }

        public decimal GTotal { get; set; }

        public Customer Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
