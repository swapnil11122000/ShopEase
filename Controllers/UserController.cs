using ECommWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;

using Microsoft.EntityFrameworkCore;

namespace ECommWeb.Controllers

{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
      
        UserDAL db;
        public readonly SignedInUser signedInUser;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new UserDAL(this.configuration);
            signedInUser = new SignedInUser();
        }


        public ActionResult Index()
        {
            List<User> model = db.GetAllUser();
            return View(model);
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            bool IsUserExists=db.IsUserExists(user);
            if (IsUserExists)
            {
                bool CredentialsRight= db.ValidateCredentials(user);
                if (CredentialsRight)
                {
                    DataTable dt = db.GetUser(user.Email.ToString());

                    int userId = Convert.ToInt32(dt.Rows[0]["UserID"]);
                          
                    HttpContext.Session.SetInt32("UserId", userId);
                    List<Claim> claims = new List<Claim>() {
                     new Claim(ClaimTypes.NameIdentifier,user.Email),
                     new Claim("Name", dt.Rows[0]["UserName"].ToString()),
                     new Claim("Role",string.IsNullOrEmpty(dt.Rows[0]["Role"].ToString()) ? "Customer" : dt.Rows[0]["Role"].ToString())         
                };
                   
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,         
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                  
                    if (!db.ValidateUserData(userId))
                    {
                        return RedirectToAction("Edit", "User", new { Id = userId });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                Vendor Vendor = new Vendor();
                Vendor.Email=user.Email;
                bool IsVendorExist = db.IsVendorExists(Vendor);
                if (IsVendorExist)
                {
                    DataTable dt = db.GetVendor(Vendor.Email.ToString());

                    int VendorID = Convert.ToInt32(dt.Rows[0]["Vendor_ID"]);
                   
                    HttpContext.Session.SetInt32("UserId", VendorID);
                    List<Claim> claims = new List<Claim>() {
                     new Claim(ClaimTypes.NameIdentifier,Vendor.Email),
                     new Claim("Name", dt.Rows[0]["Vendor_Name"].ToString()),
                     new Claim("Role","Supplier")
                };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    if (!db.ValidateData(VendorID))
                    {
                        return RedirectToAction("Edit", "Vendor",new {id=VendorID});
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
            }
            ViewData["ValidateMessage"] = "User not found";
            return View();
        }

        public IActionResult Edit(int Id)
        {
          
            var User =  db.GetUserByID(Id);
          
            return View(User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User User)
        {

            int a = db.UpdateUser(User);


            return RedirectToAction("Index", "Home");
        }


        public ActionResult SignUp()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            int res = 0;
            if (user.ProfileID==1)
            {
                Vendor Vendor = new Vendor();
                Vendor.ContactPerson=user.FirstName;
                Vendor.Email=user.Email;
                Vendor.Password=user.Password;
                res=db.AddVendor(Vendor);
            }
            else
            {
                 res = db.AddUser(user);
            }
           

            if (res > 0)
            {
                TempData["SuccessMessage"] = "Account created successfully. Please login.";
                return RedirectToAction("Login", "User");
            }
            else
            {
                TempData["AlertMessage"] = "Failed to sign up. Please try again.";
                return RedirectToAction("SignUp", "User"); 
            }
        }


        [Authorize]
        public JsonResult GetDevInfo()
        {
            Dev dev = new Dev();
            var json = JsonConvert.SerializeObject(dev);
            return Json(json);
        }

        //public ActionResult LogOut()
        //{
        //    return Redirect("http://localhost:51430/");
        //}


    }
}
