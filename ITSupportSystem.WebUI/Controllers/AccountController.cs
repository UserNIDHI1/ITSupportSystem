using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using ITSupportSystem.WebUI.session;
using Microsoft.AspNetCore.Identity;

namespace ITSupportSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ILoginService _loginService;
        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [Authentication]
        public ActionResult Index()
        {
            string res = TempData["PageSelected"] as string;
            return View();
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string retutnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Users user = _loginService.Login(model);
                if (user != null)
                {
                    Session["UserName"] = user.UserName;
                    Session["Id"] = user.Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email and Password");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}