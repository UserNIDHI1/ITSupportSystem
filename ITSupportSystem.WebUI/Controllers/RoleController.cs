using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        RoleServices _roleServices;
        public RoleController(RoleServices roleService)
        {
            _roleServices = roleService;
        }


        // GET: Admin
        public ActionResult Index()
        {
            List<Role> roles = _roleServices.GetRoleList().ToList();
            return View(roles);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            _roleServices.CreateRole(model);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(Guid Id)
        {
            RoleViewModel role = _roleServices.GetRole(Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            _roleServices.UpdateRole(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid Id)
        {
            RoleViewModel role = _roleServices.GetRole(Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(RoleViewModel model)
        {
            _roleServices.RemoveRole(model);
            return RedirectToAction("Index");
        }
    }
}
    
    