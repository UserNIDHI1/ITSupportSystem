using ITSupportSystem.Core1.Models;
using ITSupportSystem.DataAccess.SQL;
using ITSupportSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.ActionFilter
{
    public class AuditActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var logs = DependencyResolver.Current.GetService<IAuditLogServices>();
            logs.CreateAuditLog(null, HttpContext.Current.Response.StatusCode);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var errorlog = DependencyResolver.Current.GetService<IAuditLogServices>();
            if(filterContext.Exception!=null)
            {
                var statusCode = new HttpException(null, filterContext.Exception).GetHttpCode();
                errorlog.CreateAuditLog(filterContext.Exception.Message,statusCode);
            }
        }
    }
}