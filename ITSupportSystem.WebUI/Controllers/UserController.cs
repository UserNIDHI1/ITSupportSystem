using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL.Migrations;
using ITSupportSystem.Services;
using ITSupportSystem.WebUI.session;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    [Authentication]
    public class UserController : Controller
    {

        UserServices _userServices;
        RoleServices _roleServices;

        public UserController(UserServices userService, RoleServices roleServices)
        {
            this._userServices = userService;
            this._roleServices = roleServices;
        }

        // GET: User
        public ActionResult Index()
        {

            List<UserViewModel> user = _userServices.GetUserList().ToList();
            return View(user);
        }

        public ActionResult Create()
        {
            UserViewModel user = new UserViewModel();
            user.RoleDropDown = _roleServices.GetRoleList().Select(x => new DropDown()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View(user);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            var user = _userServices.CreateUser(model);
            if (user != null)
            {
                ViewBag.Message = user;
                model.RoleDropDown = _roleServices.GetRoleList().Select(x => new DropDown()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid Id)
        {
            UserViewModel user = _userServices.GetUser(Id);
            user.RoleId = _userServices.GetRoleIdByUserId(Id);
            user.RoleDropDown = _roleServices.GetRoleList().Select(x => new DropDown()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            var user = _userServices.UpdateUser(model);
            if (user != null)
            {
                ViewBag.Message = user;
                model.RoleDropDown = _roleServices.GetRoleList().Select(x => new DropDown()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid Id)
        {
            UserViewModel user = _userServices.GetUser(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {
            _userServices.RemoveUser(Id);
            return RedirectToAction("Index");
        }
    }
}