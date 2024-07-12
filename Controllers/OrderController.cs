using ECommWeb.Entities;
using ECommWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        OrderItemDAL db;
        public OrderController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            db = new OrderItemDAL(applicationDbContext);
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult PlaceOrder(Product Product)
        {
            int UserID = (int)HttpContext.Session.GetInt32("UserId");

            int OrderID = db.AddOrder(Product, UserID);

            int OrderItemID = db.AddOrderItem(Product, OrderID);
            int Result = db.SubtractQuantity(Product.OrderItem.Quantity, Product);


            TempData["SuccessMessage"] = "Order Placed Successfully";
            return RedirectToAction("MyOrders", "Order");
        }
        [Authorize]
        public IActionResult MyOrders()
        {
            int UserID= (int)HttpContext.Session.GetInt32("UserId");
            var MyOrders = db.GetMyOrders(UserID);


            return View("~/Views/Order/MyOrders.cshtml",MyOrders);
        }

    }
}

