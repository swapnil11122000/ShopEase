using ECommWeb.Entities;
using ECommWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommWeb.Controllers
{
  
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;
        VendorDAL db;

        public VendorController(ApplicationDbContext context)
        {
            this._context= context;
            db=new VendorDAL(_context);
        }
        public  IActionResult Index()
        {
            List<Vendor> Vendor = db.GetAllVendor(); ;

           
            return View(Vendor);
        }
        public IActionResult Details(int id)
        {

            var Vendor = GetVendorById(id);
            var partialGridData = db.GetVendorProducts();
            ViewBag.PartialGridData = partialGridData;
            return View(Vendor);
        }
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
        new SelectListItem { Value = "1", Text = "Clothing" },
        new SelectListItem { Value = "2", Text = "Electronics" },
        new SelectListItem { Value = "3", Text = "Automobile" },
         new SelectListItem { Value = "4", Text = "Fashion" },
          new SelectListItem { Value = "5", Text = "Appliances" },
           new SelectListItem { Value = "6", Text = "Grocery" },
            new SelectListItem { Value = "7", Text = "Home & Furniture" },
             new SelectListItem { Value = "8", Text = "Toys" },

    };

            ViewBag.Categories = new SelectList(categories, "Value", "Text");

            var MyVendors = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Vendor1" },
        new SelectListItem { Value = "2", Text = "Vendor2" },
        new SelectListItem { Value = "3", Text = "Vendor3" },
         new SelectListItem { Value = "4", Text = "Vendor4" },
          new SelectListItem { Value = "5", Text = "Vendor5" },
           new SelectListItem { Value = "6", Text = "Vendor6" },
            new SelectListItem { Value = "7", Text = "Vendor7" },
             new SelectListItem { Value = "8", Text = "Vendor8" },

    };

            ViewBag.MyVendors = new SelectList(MyVendors, "Value", "Text");
            return View("SellProducts");
        }

        public IActionResult AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;
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
