using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.WebUI.ActionFilter;
using ITSupportSystem.WebUI.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    [Authentication]
    [AuditActionFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}