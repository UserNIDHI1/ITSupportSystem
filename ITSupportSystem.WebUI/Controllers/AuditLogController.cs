using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using ITSupportSystem.WebUI.ActionFilter;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    public class AuditLogController : Controller
    {
        AuditLogServices _auditLogServices;
        public AuditLogController(AuditLogServices auditLogServices)
        {
            _auditLogServices = auditLogServices;
        }

        //For ActivityLog
        public ActionResult ActivityIndex()
        {
            List<AuditLogViewModel> activityviewmodel = _auditLogServices.GetauditlogList().ToList();
            return View(activityviewmodel);
        }

        public ActionResult GetAllActivityJson([DataSourceRequest] DataSourceRequest request)
        {
            List<AuditLogViewModel> auditLogViewModel = _auditLogServices.GetauditlogList().ToList();
            return Json(auditLogViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActivityDetails(Guid Id)
        {
            AuditLogViewModel auditactivityLogViewModel = _auditLogServices.GetAuditLog(Id);
            return View(auditactivityLogViewModel);
        }


        //for ErrorLog
        public ActionResult ErrorIndex(bool IsException = true)
        {
            List<AuditLogViewModel> errorlog = _auditLogServices.GetauditlogList(IsException).ToList();
            return View(errorlog);
        }

        public ActionResult GetAllErrorJson([DataSourceRequest] DataSourceRequest request, bool IsException = true)
        {
            List<AuditLogViewModel> errorLogViewModel = _auditLogServices.GetauditlogList(IsException).ToList();
            return Json(errorLogViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //show details about the ErrorLog
        public ActionResult ErrorDetails(Guid Id)
        {
            AuditLogViewModel auditLogViewModel = _auditLogServices.GetAuditLog(Id);
            return View(auditLogViewModel);
        }
    }
}