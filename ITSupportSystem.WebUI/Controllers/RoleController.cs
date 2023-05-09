using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using ITSupportSystem.WebUI.session;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    [Authentication]
    public class RoleController : Controller
    {
        RoleServices _roleServices;
        PermissionServices _permissionServices;

        public RoleController(RoleServices roleService, PermissionServices permissionServices)
        {
            _roleServices = roleService;
            _permissionServices = permissionServices;
        }

        public ActionResult Index()
        {
            List<Role> roles = _roleServices.GetRoleList().ToList();
            return PartialView("_RoleIndexPartial");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            var role = _roleServices.CreateRole(model);
            if (role != null)
            {
                ViewBag.Message = role;
                return View(model);
            }
            else
            {
                TempData["PageSelected"] = "RoleManagement";
                return RedirectToAction("Index", "Account");
            }
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
            var role = _roleServices.UpdateRole(model);
            if (role != null)
            {
                ViewBag.Message = role;
                return View(model);
            }
            else
            {
                TempData["PageSelected"] = "RoleManagement";
                return RedirectToAction("Index", "Account");
            }
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

        //json
        public ActionResult GetAllRolesJson([DataSourceRequest] DataSourceRequest request)
        {
            List<RoleViewModel> userRoleViewModels = _roleServices.GetRoleList().Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name, Code = x.Code }).ToList();
            return Json(userRoleViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Permission(Guid Id)
        {
            ViewBag.RoleId = Id;
            return View();
        }

        //json method for permission
        public ActionResult GetAllPermissionJson([DataSourceRequest] DataSourceRequest request, Guid RoleId)
        {
            List<PermissionViewModel> permissionViewModels = _permissionServices.GetPermission(RoleId).ToList();
            return Json(permissionViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePermission(List<Permission> model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                TempData["PageSelected"] = "RoleManagement";
                _permissionServices.UpdatePermission(model);
                var permission = _permissionServices.GetPermission((Guid)Session["RoleId"]).ToList();
                Session["Permission"] = permission;
                return Content("true");
                
            }
        }
    }
}

