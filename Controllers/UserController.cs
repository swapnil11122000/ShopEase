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
      
        UsersDAL db;
        public readonly SignedInUser signedInUser;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new UsersDAL(this.configuration);
            signedInUser = new SignedInUser();
        }


        public ActionResult Index()
        {
            List<Users> model = db.GetAllUsers();
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
        public async Task<IActionResult> Login(Users user)
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
                Vendors vendors = new Vendors();
                vendors.Email=user.Email;
                bool IsVendorExist = db.IsVendorExists(vendors);
                if (IsVendorExist)
                {
                    DataTable dt = db.GetVendor(vendors.Email.ToString());

                    int VendorID = Convert.ToInt32(dt.Rows[0]["Vendor_ID"]);
                   
                    HttpContext.Session.SetInt32("UserId", VendorID);
                    List<Claim> claims = new List<Claim>() {
                     new Claim(ClaimTypes.NameIdentifier,vendors.Email),
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
                        return RedirectToAction("Edit", "Vendors",new {id=VendorID});
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
          
            var Users =  db.GetUserByID(Id);
          
            return View(Users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Users users)
        {

            int a = db.UpdateUser(users);


            return RedirectToAction("Index", "Home");
        }


        public ActionResult SignUp()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Users user)
        {
            int res = 0;
            if (user.Role==true)
            {
                Vendors vendors = new Vendors();
                vendors.Vendor_Name=user.UserName;
                vendors.Email=user.Email;
                vendors.Password=user.Password;
                res=db.AddVendor(vendors);
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
