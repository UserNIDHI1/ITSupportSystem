using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL;
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
    public class FormController : Controller
    {
        FormServices _formServices;
        PermissionServices _permissionServices;

        public FormController(FormServices formService,PermissionServices permissionServices)
        {
            _formServices = formService;
            _permissionServices = permissionServices;
        }

        [Authentication]
        public ActionResult Index()
        {
            
            List<FormViewModel> formviewmodel = _formServices.GetFormList().ToList();
            return View(formviewmodel);
        }

        public ActionResult GetAllFormJson([DataSourceRequest] DataSourceRequest request)
        {
            List<FormViewModel> formViewModels = _formServices.GetFormList().ToList();
            return Json(formViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            FormViewModel form = new FormViewModel();
            form.FormDropDown = _formServices.GetFormList().Select(x => new DropdownForm()
            {
                ParentFormId = x.Id,
                ParentFormName = x.Name
            }).ToList();
            return View(form);
        }

        [HttpPost]
        public ActionResult Create(FormViewModel model)
        {
            var form = _formServices.CreateForm(model);
            if (form != null)
            {
                ViewBag.Message = form;
                model.FormDropDown = _formServices.GetFormList().Select(x => new DropdownForm()
                {
                    ParentFormId = x.Id,
                    ParentFormName = x.Name
                }).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Form");
            }
        }

        public ActionResult Edit(Guid Id)
        {
            FormViewModel form = _formServices.GetForm(Id);
            form.FormDropDown = _formServices.GetFormList().Select(x => new DropdownForm()
            {
                ParentFormId = x.Id,
                ParentFormName = x.Name
            }).ToList();
            return View(form);
        }

        [HttpPost]
        public ActionResult Edit(FormViewModel model)
        {

            var form = _formServices.UpdateForm(model);
            var permission = _permissionServices.GetPermission((Guid)Session["RoleId"]).ToList();
            Session["Permission"] = permission;
            if (form != null)
            {
                ViewBag.Message = form;
                model.FormDropDown = _formServices.GetFormList().Select(x => new DropdownForm()
                {
                    ParentFormId = x.Id,
                    ParentFormName = x.Name
                }).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Form");
            }
        }

        public ActionResult Delete(Guid Id)
        {
            FormViewModel form = _formServices.GetForm(Id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {
            _formServices.RemoveForm(Id);  
            return RedirectToAction("Index");
        }
    }
}