using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderingSystem.Models.ViewModel
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public string OrderName { get; set; }


        public OrderedItem OrderedItem { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Unit> Units { get; set; }


        public int? UnitId { get; set; }

        public string UnitType { get; set; }

        public int? OrderId { get; set; }
        public decimal TotalPrice { get; set; }





        public int? ItemId { get; set; }
        public decimal Sub_Total { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
