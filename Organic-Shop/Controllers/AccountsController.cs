using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic_Shop.Helpper;
using Organic_Shop.Models;
using Organic_Shop.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Organic_Shop.Extensions;

namespace Organic_Shop.Controllers
{
    public class AccountsController : Controller
    {
        private readonly OrganicShopContext _context;

        public AccountsController(OrganicShopContext context)
        {
            this._context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("register.html", Name ="Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string phone)
        {
            try
            {
                var customer = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == phone.ToLower());
                if(customer != null)
                {
                    return Json(data: "Phone " + phone + "has been existed !!");
                }
                return Json(data: "true");
            }
            catch
            {
                return Json(data: "true");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string email)
        {
            try
            {
                var customer = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
                if (customer != null)
                {
                    return Json(data: "Email " + email + "has been existed !!");
                }
                return Json(data: "true");
            }
            catch
            {
                return Json(data: "true");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("register.html", Name = "Register")]
        public async Task<IActionResult> RegisterUser(RegisterVM registerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Customer customer = new Customer
                    {
                        FullName = registerVM.FullName,
                        Phone = registerVM.Phone.Trim().ToLower(),
                        Email = registerVM.Email.Trim().ToLower(),
                        Password = (registerVM.Password + salt.Trim()).CreateMD5(),
                        Active = true,
                        Salt = salt,
                        CreateDate = DateTime.Now

                    }; 
                    try
                        {
                            _context.Add(customer);
                            await _context.SaveChangesAsync();
                            HttpContext.Session.SetString("CustomerId", customer.CustomerId.ToString());
                            var accountId = HttpContext.Session.GetString("CustomerId");

                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, customer.FullName),
                                new Claim("CustomerId", customer.CustomerId.ToString())
                            };
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);
                            return RedirectToAction("Dashboard", "Accounts");
                        }
                    catch
                        {
                        return RedirectToAction("RegisterUser", "Accounts");
                        }
                }
                else
                {
                    return View(registerVM);
                }
            }
            catch
            {
                return View(registerVM);
            }
        }

        [AllowAnonymous]
        [Route("login.html", Name ="Login")]
        public IActionResult Login(string returnUrl = null)
        {
            var accountID = HttpContext.Session.GetString("CustomerId");
            if(accountID != null)
            {
                return RedirectToAction("Dashboard", "Accounts");
            }
            return View();
        }

        //public async Task<IActionResult> Login(LoginVM loginVM, string returnUrl = null)
        //{

        //}
    }   
}
