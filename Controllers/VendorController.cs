using ECommWeb.Entities;
using ECommWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Vendor == null)
            {
                return NotFound();
            }

            var Vendor = await _context.Vendor.FindAsync(id);
            if (Vendor == null)
            {
                return NotFound();
            }
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
            return View("SellProducts");
        }
        public ActionResult ExistingProduct()
        {
            return View("ExistingProduct");
        }
        public ActionResult VendorProfile()
        {
            var vendor = _context.Vendor
                           .Include(v => v.Product) 
                           .FirstOrDefault(v => v.VendorID == 1);

            if (vendor == null)
            {
                return NotFound(); // Handle case where vendor is not found
            }

           
            return View("VendorProfile",vendor);
        }
    }
}
