using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using ITSupportSystem.WebUI.ActionFilter;
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
    [AuditActionFilter]
    public class CommonLookUpController : Controller
    {
        ICommonLookUpServices _commonLookServices;
        public CommonLookUpController(ICommonLookUpServices commonLookServices)
        {
            _commonLookServices = commonLookServices;
        }

        public ActionResult Index()
        {
            List<CommonLookUp> commonlook = _commonLookServices.GetCommonLookUpList().ToList();
            return PartialView("_CommonLookUpIndexPartial");
        }

        public ActionResult Create()
        {
            CommonLookUpViewModel commonLookUp = new CommonLookUpViewModel();
            return PartialView("CreatePartialView", commonLookUp);

        }

        [HttpPost]
        public ActionResult Create(CommonLookUp model)
        {
            CommonLookUp commonLookUp = _commonLookServices.CreateCommonLookUp(model);
            TempData["PageSelected"] = "Settings";
            if (commonLookUp != null)
            {
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }

        public ActionResult Edit(Guid Id)
        {
            CommonLookUpViewModel commonlook = _commonLookServices.GetCommonLookUp(Id);
            if (commonlook == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView("EditPartialView", commonlook);
            }
        }

        [HttpPost]
        public ActionResult Edit(CommonLookUp model)
        {
            CommonLookUp commonLookUp = _commonLookServices.UpdateCommonLookUp(model);
            TempData["PageSelected"] = "Settings";
            if (commonLookUp != null)
            {
                return Content("True");
            }
            else
            {
                return Content("False");
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            TempData["PageSelected"] = "Settings";
            _commonLookServices.RemoveCommonLookUp(Id);
            return RedirectToAction("Index");
        }

        public ActionResult GetCommonLookUpJson([DataSourceRequest] DataSourceRequest request)
        {
            List<CommonLookUpViewModel> commonlookupViewModels = _commonLookServices.GetCommonLookUpList().Select(x => new CommonLookUpViewModel() { Id = x.Id, ConfigName = x.ConfigName, ConfigKey = x.ConfigKey, ConfigValue = x.ConfigValue, DisplayOrder = x.DisplayOrder, Description = x.Description }).ToList();
            return Json(commonlookupViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


    }
}