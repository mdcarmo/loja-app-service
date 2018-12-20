using System.Collections.Generic;

namespace LojaAppApi.Model
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}
