using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderingSystem.Models.ViewModel
{
    public class ItemUnitVM
    {
        public Item Item { get; set; }
        public IList<SelectListItem> Unit { get; set; }
        public UnitItem UnitItems { get; set; }
    }
}
