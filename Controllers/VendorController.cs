using ECommWeb.Entities;
using ECommWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommWeb.Controllers
{
    public class VendorController : Controller
    {
        private readonly ApplicationDbContext _context;
        VendorsDAL db;
        public VendorController(ApplicationDbContext _Context)
        {
            this._context = _Context;
            db = new VendorsDAL(_Context);
        }
        // GET: VendorController
        public ActionResult Index()
        {
            List<Vendors> vendors = db.GetAllVendors();
            return View(vendors);
        }

        // GET: VendorController/Details/5
        public ActionResult Details(int id)
        {
            var model = db.GetVendorByID(id);
            return View(model);
        }

        // GET: VendorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendors vendor)
        {
            try
            {
                int result = db.AddVendor(vendor);
                if (result > 0)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Something Went Wrong";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = db.GetVendorByID(id);
            return View(model);
        }

        // POST: VendorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendors vendors)
        {
            try
            {
                int result = db.UpdateVendor(vendors);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.error = "Something went wrong";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorController/Delete/5
        public ActionResult Delete(int id)
        {
            var model=db.GetVendorByID(id);
            return View();
        }

        // POST: VendorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                int result = db.DeleteVendor(id);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.error = "Something went wrong";
                    return View();
                }
               
            }
            catch
            {
                return View();
            }
        }
    }
}
