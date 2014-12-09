using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DriveIT.Models;
using DriveIT.WebAPI.Models;
using DriveIT.WebAPI.Providers;
using DriveIT.WebAPI.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using DriveIT.WebAPI.Controllers;

namespace DriveIT.MVC.Controllers
{
    [Authorize]
    public class AccntController : AsyncController
    {
        private AccountController controller = new AccountController();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password)
        {
             var client = new HttpClient { BaseAddress = new Uri("http://localhost:5552/api/") };

            var dict = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", email},
                {"password", password}
            };

            await client.PostAsync("Token", new FormUrlEncodedContent(dict));
          
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterBindingModel model)
        {
            await controller.Register(model);
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            controller.Logout();
            return RedirectToAction("Index", "Home");
        }

    }
}