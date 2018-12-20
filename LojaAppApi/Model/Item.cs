using System.Collections.Generic;

namespace LojaAppApi.Model
{
    public class Item
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
