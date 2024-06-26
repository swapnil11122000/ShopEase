﻿using ECommWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommWeb.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommWeb.Controllers

{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;
        ProductDAL db;

        public ProductController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            db = new ProductDAL(applicationDbContext);
        }


        // GET: ProductController
        //display all products
        public ActionResult Index()
        {
            List<Product> model = db.GetAllProducts();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Supplier()
        {
            List<Product> model = db.GetAllProducts();
            return View(model);
        }


        public IActionResult Search(string product)
        {
            var model = db.SearchProductByName(product);
            return View(model);
        }
        // GET: ProductController/Details/5
        //display single product
        public ActionResult Details(int Id)
        {
            var model = db.GetProductById(Id);
            return View(model);
        }

        public ActionResult AddtoCart(int Id)
        {
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            Cart cart = new Cart();
            cart.UserID = userid;
            cart.ProductID = Id;
            db.InsertItemToCart(cart);
            return RedirectToAction("Index", "Carts");

        }

        public ActionResult Create()
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
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod)
        {
            try
            {
                prod.Vendor_ID = (int)HttpContext.Session.GetInt32("UserId");
                int res = db.AddProduct(prod);
                if (res > 0)
                {
                    return RedirectToAction(nameof(Index));

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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int Id)
        {
            var model = db.GetProductById(Id);
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product prod)
        {
            try
            {
                int res = db.UpdateProduct(prod);
                if (res > 0)
                {
                    return RedirectToAction(nameof(Supplier));

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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int Id)
        {
            var model = db.GetProductById(Id);
            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int Id)
        {
            try
            {
                int res = db.DeleteProduct(Id);
                if (res > 0)
                {
                    return RedirectToAction(nameof(Index));

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
    }
}
