using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITSupportSystem.Services
{
    public interface IAuditLogServices
    {
        string CreateAuditLog(string message,int statusCode);
        List<AuditLogViewModel> GetauditlogList(bool IsException=false);
        AuditLogViewModel GetAuditLog(Guid Id);

    }


    public class AuditLogServices : IAuditLogServices
    {
        IAuditLogRepository _auditLogRepository;

        public AuditLogServices(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public string CreateAuditLog(string message,int statusCode)
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);

            AuditLog audit = new AuditLog()
            {
                Id = Guid.NewGuid(),
                UserId = (Guid)HttpContext.Current.Session["Id"],
                ClientIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                Url = currentContext.Request.Url.ToString(),
                ExecutionTime = DateTime.UtcNow,
                ExecutionDuration = DateTime.UtcNow.Millisecond,
                HttpMethod = currentContext.Request.HttpMethod,
                HttpStatusCode = statusCode,
                BrowserInfo = currentContext.Request.Browser.Browser + "" + HttpContext.Current.Request.Browser.Version,
                Comments = "Controller: " + routeData.Values["controller"].ToString() + " || Action: " + routeData.Values["action"].ToString(),
                Parameters = routeData.Values["id"].ToString(),
                Exception=message
            };
            _auditLogRepository.Insert(audit);
            _auditLogRepository.commit();
            return null;
        }

        public AuditLogViewModel GetAuditLog(Guid Id)
        {
            return _auditLogRepository.GetAuditLogById(Id);
        }

        public List<AuditLogViewModel> GetauditlogList(bool IsException = false)
        {
            return _auditLogRepository.GetAuditLogList(IsException);
        }
    }
}
