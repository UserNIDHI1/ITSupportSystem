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
        public RoleController(RoleServices roleService)
        {
            _roleServices = roleService;
        }


        // GET: Admin
        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            List<Role> roles = _roleServices.GetRoleList().ToList();
            return View(roles.ToDataSourceResult(request));
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

            var role = _roleServices.UpdateRole(model);
            if (role != null)
            {
                ViewBag.Message = role;
                return View(model);
            }
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

        public ActionResult GetAllRolesJson([DataSourceRequest] DataSourceRequest request)
        {
            List<RoleViewModel> userRoleViewModels = _roleServices.GetRoleList().Select(x => new RoleViewModel() { Id = x.Id, Name = x.Name, Code = x.Code }).ToList();
            return Json(userRoleViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}

