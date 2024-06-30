using ECommWeb.Entities;
using ECommWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommWeb.Controllers
{
    [Authorize]
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        VendorsDAL db;

        public VendorsController(ApplicationDbContext context)
        {
            this._context= context;
            db=new VendorsDAL(_context);
        }
        public  IActionResult Index()
        {
            List<Vendors> Vendors = db.GetAllVendors(); ;

           
            return View(Vendors);
        }
        public IActionResult Details(int id)
        {

            var vendors = GetVendorById(id);
            var partialGridData = db.GetVendorProducts();
            ViewBag.PartialGridData = partialGridData;
            return View(vendors);
        }
        public Vendors GetVendorById(int Id)
        {
            var result = _context.Vendors.Where(x => x.Vendor_ID == Id).FirstOrDefault();
            return result;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendors vendors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendors);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendors);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendors = await _context.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return NotFound();
            }
            return View(vendors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit( Vendors vendors)
        {

            int a = db.UpdateVendor(vendors);


            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendors = await _context.Vendors
                .FirstOrDefaultAsync(m => m.Vendor_ID == id);
            if (vendors == null)
            {
                return NotFound();
            }

            return View(vendors);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vendors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vendors'  is null.");
            }
            var vendors = await _context.Vendors.FindAsync(id);
            if (vendors != null)
            {
                _context.Vendors.Remove(vendors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorsExists(int id)
        {
          return (_context.Vendors?.Any(e => e.Vendor_ID == id)).GetValueOrDefault();
        }
    }
}
