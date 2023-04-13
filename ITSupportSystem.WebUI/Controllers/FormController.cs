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
    public class FormController : Controller
    {
        

        FormServices _formServices;
        public FormController(FormServices formService)
        {
            _formServices = formService;
        }

        [Authentication]

        // GET: Form
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
        public ActionResult ConfirmDelete(FormViewModel model)
        {
            _formServices.RemoveForm(model);
            return RedirectToAction("Index");
        }
    }
}