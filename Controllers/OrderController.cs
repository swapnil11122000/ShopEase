using ECommWeb.Entities;
using ECommWeb.Models;
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


        public IActionResult PlaceOrder(OrderItem orderItem)
        {

            // int a = db.UpdateVendor(Vendor);


            return RedirectToAction("Index", "Home");
        }
    }
}
