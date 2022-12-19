using System.ComponentModel.DataAnnotations;

namespace OrderingSystem.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }

        public decimal TotalPrice { get; set; }
        public virtual ICollection<OrderedItem> OrderItem { get; set; }

    }
}
