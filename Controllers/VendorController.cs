using ECommWeb.Entities;
using ECommWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommWeb.Controllers
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;
        VendorDAL db;

        public VendorController(ApplicationDbContext context)
        {
            this._context= context;
            db=new VendorDAL(_context);
        }
        [Authorize]
        public  IActionResult Index()
        {
            List<Vendor> Vendor = db.GetAllVendor(); ;

           
            return View(Vendor);
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            var Vendor = db.GetVend(id);
            //var Vendor = GetVendorById(id);
            //var partialGridData = db.GetVendorProducts();
            //ViewBag.PartialGridData = partialGridData;
            return View(Vendor);
        }
        [Authorize]
        public Vendor GetVendorById(int Id)
        {
            var result = _context.Vendor.Where(x => x.VendorID == Id).FirstOrDefault();
            return result;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendor Vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Vendor);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Vendor);
        }
        public IActionResult Edit(int id)
        {
           
            var Vendor = db.GetVendorByID(id);
          
            return View(Vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit( Vendor Vendor)
        {

            int a = db.UpdateVendor(Vendor);


            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vendor == null)
            {
                return NotFound();
            }

            var Vendor = await _context.Vendor
                .FirstOrDefaultAsync(m => m.VendorID == id);
            if (Vendor == null)
            {
                return NotFound();
            }

            return View(Vendor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vendor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vendor'  is null.");
            }
            var Vendor = await _context.Vendor.FindAsync(id);
            if (Vendor != null)
            {
                _context.Vendor.Remove(Vendor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
          return (_context.Vendor?.Any(e => e.VendorID == id)).GetValueOrDefault();
        }

        public ActionResult SellProducts()
        {
            var categories = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Electronics" },
        new SelectListItem { Value = "2", Text = "Clothing" },
        new SelectListItem { Value = "3", Text = "Books" },
         new SelectListItem { Value = "4", Text = "Home Appliances" },
          new SelectListItem { Value = "5", Text = "Toys" },
           new SelectListItem { Value = "6", Text = "Sports" },
            new SelectListItem { Value = "7", Text = "Beauty" },
             new SelectListItem { Value = "8", Text = "Furniture" },
             new SelectListItem { Value = "9", Text = "Food" },
             new SelectListItem { Value = "10", Text = "Garden" },
            

    };

            ViewBag.Categories = new SelectList(categories, "Value", "Text");

         
            return View("SellProducts");
        }

            public IActionResult AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;
            product.VendorID= (int)HttpContext.Session.GetInt32("UserId");
            _context.Products.Add(product);
            _context.SaveChanges();

           return RedirectToAction("VendorProfile","Vendor");
        }
        public ActionResult ExistingProduct()
        {
            return View("ExistingProduct");
        }
       
        public ActionResult VendorProfile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            var result = db.GetVend(userId);


            return View("VendorProfile", result);
        }
    }
}
