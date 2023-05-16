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
        private PermissionServices _permissionServices;
        private IRepository<UserRole> _userroleRepository;
        

        public AccountController(ILoginService loginService,PermissionServices permissionServices, IRepository<UserRole> userroleRepository)
        {
            _loginService = loginService;
            _permissionServices = permissionServices;
            _userroleRepository = userroleRepository;
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
            Session.Abandon();  //session remove
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
                    //store UserName and Id store in session
                    Session["UserName"] = user.UserName;
                    Session["Id"] = user.Id;
                    Guid RoleId = _userroleRepository.Collection().Where(x => x.UserId == user.Id).Select(x => x.RoleId).FirstOrDefault();
                    Session["RoleId"] = RoleId;
                    var permission = _permissionServices.GetPermission(RoleId).ToList();
                    Session["Permission"] = permission;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                    return View();
                }
            }
        }


        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}