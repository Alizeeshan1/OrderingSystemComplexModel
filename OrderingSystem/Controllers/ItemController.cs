using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OrderingSystem.Data;
using OrderingSystem.Models;
using OrderingSystem.Models.ViewModel;
namespace OrderingSystem.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ItemController(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["UnitTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "UnitType_desc" : "";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var iTEMS = (from item in _db.Items

                         join ItemUnit in _db.UnitItems on item.Id equals ItemUnit.ItemId
                         join unit in _db.Units on ItemUnit.UnitId equals unit.UnitId
                         select new ItemViewModel
                         {
                             ItemId = item.Id,
                             UnitId = unit.UnitId,
                             ItemName = item.Name,
                             Price = item.Price,
                             UnitType = unit.UnitType,
                         });
            if (!String.IsNullOrEmpty(searchString))
            {
                //iTEMS
                iTEMS = iTEMS.Where(i => i.ItemName.Contains(searchString)
                                       || i.UnitType.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    iTEMS = iTEMS.OrderByDescending(i => i.ItemName);
                    break;
                case "UnitType_desc":
                    iTEMS = iTEMS.OrderByDescending(i => i.UnitType);
                    break;
                case "price_desc":
                    iTEMS = iTEMS.OrderByDescending(i => i.Price);
                    break;
                default:
                    iTEMS = iTEMS.OrderBy(i => i.ItemName);
                    break;
            }
            return View(iTEMS);
        }

        
        [HttpGet]
        public async Task<IActionResult> CreateItem()
        {
            ItemUnitVM itemUnitVM = new ItemUnitVM();
            var unit = _db.Units.Select(x => new SelectListItem()
            {
                Text = x.UnitType,
                Value = x.UnitId.ToString(),
            }).ToList();
            itemUnitVM.Unit = unit;

            return View(itemUnitVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemUnitVM model)
        {
            
            var item = new Item
            {
                Name = model.Item.Name,
                Price = model.Item.Price,
                
            };
         
            var unit = model.Unit.Where(x => x.Selected).Select(y => y.Value).ToList();
         
            foreach (var i in unit)
            {
                item.UnitItems.Add(new UnitItem()
                {
                    UnitId = int.Parse(i),
                });
            };
               await _db.AddAsync(item);
               await _db.SaveChangesAsync();
               return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
             var item = _db.Items.Include(p => p.UnitItems).Where(p => p.Id == id).FirstOrDefault();

              var selectedunit = item.UnitItems.Select(x => x.UnitId).ToList();
              var unit = _db.Units.Select(x => new SelectListItem()
              {
                  Text = x.UnitType,
                  Value = x.UnitId.ToString(),
                  Selected = selectedunit.Contains(x.UnitId)
              }).ToList();
              ItemUnitVM itemUnitVM = new ItemUnitVM();
              itemUnitVM.Unit = unit;
              itemUnitVM.Item = item;

              return View(itemUnitVM);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemUnitVM model, int? Id)
        {

            var item = _db.Items.Include(p => p.UnitItems).Where(p => p.Id == Id).FirstOrDefault();
            item.Name = model.Item.Name;
            item.Price = model.Item.Price;

            var existingunitID = item.UnitItems.Select(x => x.UnitId).ToList();

            var selectedunitID = model.Unit.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();

            var ToAdd = selectedunitID.Except(existingunitID);
            var ToRemove = existingunitID.Except(selectedunitID);
            item.UnitItems = item.UnitItems.Where(x => !ToRemove.Contains(x.UnitId)).ToList();

            foreach (var itm in ToAdd)
            {
                item.UnitItems.Add(new UnitItem()
                {
                    UnitId = itm
                });
            }
                _db.Items.Update(item);
                _db.SaveChanges();
                return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var iTEMS = _db.Items.Find(id);
            _db.Items.Remove(iTEMS);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


//Git Changes