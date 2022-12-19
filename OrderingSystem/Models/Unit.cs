using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OrderingSystem.Models
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitType { get; set; }
        public virtual ICollection<UnitItem> UnitItems { get; set; } = new HashSet<UnitItem>();
        public virtual ICollection<OrderedItem> OrderedItems { get; set; } = new HashSet<OrderedItem>();


    }
}
