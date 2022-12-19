using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderingSystem.Data;
using OrderingSystem.Models;
using OrderingSystem.Models.ViewModel;

namespace OrderingSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
     
        public OrderController(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult> IndexOrder(string sortOrder, string searchString)
        {
            ViewData["OrderNameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "OrderName_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            ViewData["CurrentFilter"] = searchString;

            var orders = await _db.Orders.ToListAsync();


            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.OrderName.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "OrderName_desc":
                    orders = orders.OrderByDescending(s => s.OrderName).ToList();
                    break;
                case "Price":
                    orders = orders.OrderBy(s => s.TotalPrice).ToList();
                    break;
                case "Price_desc":
                    orders = orders.OrderBy(s => s.TotalPrice).ToList();
                    break;
                case "Date":
                    orders = orders.OrderBy(s => s.OrderDate).ToList();
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.OrderDate).ToList();
                    break;
                default:
                    orders = orders.OrderBy(s => s.OrderName).ToList();
                    break;
            }
            return View(orders);

        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order, int? Id)
        {
            
                order.OrderDate = DateTime.UtcNow;
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexOrder");

        }

        [HttpGet]
        public async Task<IActionResult> EditOrder(int? Id)
        {
            if (Id is not null)
            {
                var order = await _db.Orders.FindAsync(Id);
                if (order is null)
                {
                    return NotFound("Order does not exist");
                }
                return View("EditOrder", order);
            }
            return NotFound("Order does not exist");
        }
        [HttpPost]
        public async Task<IActionResult> EditOrder(Order order)
        {
                var odr = await _db.Orders.FindAsync(order.OrderId);
       
                odr.OrderName = order.OrderName;
                _db.Orders.Update(odr);
                await _db.SaveChangesAsync();
                return RedirectToAction("IndexOrder");
        }


        [HttpGet]
        public async Task<IActionResult> AddItems(int? id)
        {
            var items = (from item in _db.Items

                         join ItemUnit in _db.UnitItems on item.Id equals ItemUnit.ItemId
                         join unit in _db.Units on ItemUnit.UnitId equals unit.UnitId
                         select new OrderViewModel
                         {
                             ItemId = item.Id,
                             UnitId = unit.UnitId,
                             UnitType = unit.UnitType,
                             ItemName = item.Name,
                             Price = item.Price,
                             OrderId = id

                         }).ToList();

            return View(items);
        }



        [HttpPost]
        public async Task<IActionResult> AddItems(OrderViewModel orderViewModels, int? id)
        {
            var orderID = orderViewModels.OrderId;
            var model = await _db.Orders.FindAsync(orderID);
             var price = await _db.Items.FindAsync(orderViewModels.ItemId);

                var orderedItem = await _db.OrderedItems.FindAsync(orderID, orderViewModels.ItemId);
                
                    var orderItem = new OrderedItem()
                    {
                        ItemId_Fk = orderViewModels.ItemId,
                        UnitId_Fk = orderViewModels.UnitId,
                        Quantity = orderViewModels.Quantity,
                        Sub_Total = price.Price * orderViewModels.Quantity,
                        OrderId_FK = orderViewModels.OrderId,
                    };
                    model.TotalPrice += orderItem.Sub_Total;
                    _db.Update(model);

                    await _db.AddAsync(orderItem);
                    await _db.SaveChangesAsync();

            return RedirectToRoute(orderID);
        }

        [HttpGet]
        public async Task<IActionResult> EditOrderItem(int? id)
        {
            var order = _db.OrderedItems.ToList();

            List<OrderViewModel> Ordered = new List<OrderViewModel>();

            foreach (var item in order)
            {
                if (item.OrderId_FK == id)
                {

                    var units = _db.Units.Where(x => x.UnitId == item.UnitId_Fk).FirstOrDefault();
                    var ItemName = _db.Items.Where(x => x.Id == item.ItemId_Fk).FirstOrDefault();

                    var x = new OrderViewModel()
                    {
                        ItemId = item.ItemId_Fk,
                        UnitId = item.UnitId_Fk,
                        UnitType = units.UnitType,
                        ItemName = ItemName.Name,
                        Price = ItemName.Price,
                        Quantity = item.Quantity,
                        Sub_Total = item.Sub_Total,
                        OrderId = id,
                    };
                    Ordered.Add(x);
                }
            }

            return View(Ordered);
        }


        [HttpPost]
        public async Task<IActionResult> EditOrderItem(OrderViewModel orderViewModels)
        {
            var orderID = orderViewModels.OrderId;
            var model = await _db.Orders.FindAsync(orderID);

                var price = await _db.Items.FindAsync(orderViewModels.ItemId);

                var orderedItem = await _db.OrderedItems.FindAsync(orderID, orderViewModels.ItemId);

                if (orderedItem != null)
                {
                    decimal price_to_sub = orderedItem.Sub_Total;

                    orderedItem.ItemId_Fk = orderViewModels.ItemId;
                    orderedItem.UnitId_Fk = orderViewModels.UnitId;
                    orderedItem.Quantity = orderViewModels.Quantity;
                    orderedItem.Sub_Total = price.Price * orderViewModels.Quantity;
                    orderedItem.OrderId_FK = orderViewModels.OrderId;

                    model.TotalPrice -= price_to_sub;
                    model.TotalPrice += orderedItem.Sub_Total;
                    _db.Update(model);

                    _db.Update(orderedItem);
                    await _db.SaveChangesAsync();
                    return RedirectToRoute(orderID);

                }
            return RedirectToRoute(orderID);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(OrderViewModel orderViewModels)
        {
                var model = await _db.Orders.FindAsync(orderViewModels.OrderId);
                var orderedItem = await _db.OrderedItems.FindAsync(orderViewModels.OrderId, orderViewModels.ItemId);

                model.TotalPrice -= orderedItem.Sub_Total;
                _db.Update(model);
                _db.Remove(orderedItem);
                await _db.SaveChangesAsync();
                return RedirectToAction("EditOrderItem", new { @id = orderViewModels.OrderId });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            var order = await _db.Orders.FindAsync(Id);
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return RedirectToAction("IndexOrder");
                
        }

    }


}

