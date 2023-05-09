using ITSupportSystem.Core1;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    public class TicketController : Controller
    {
        ITicketServices _ticketServices;
        IUserServices _userServices;

        public TicketController(ITicketServices ticketServices, IUserServices userServices)
        {
            _ticketServices = ticketServices;
            _userServices = userServices;
        }
        public ActionResult Index()
        {
            List<TicketViewModel> ticketViewModels = _ticketServices.GetTicketList().OrderByDescending(x => x.CreatedOn).ToList();
            return View(ticketViewModels);
        }

        public ActionResult Create()
        {
            TicketViewModel ticketViewModel = new TicketViewModel();
            ticketViewModel.PriorityDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Priority);
            ticketViewModel.StatusDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Status);
            ticketViewModel.TypeDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Type);
            ticketViewModel.AssignedDropDown = _userServices.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            return View(ticketViewModel);
        }

        [HttpPost]
        public ActionResult Create(TicketViewModel model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //Save the image
                model.Image = model.Id + "_" + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//TicketAttachment//") + model.Image);

                model.PriorityDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Priority);
                model.StatusDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Status);
                model.TypeDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Type);
                model.AssignedDropDown = _userServices.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
                
            }
            Ticket ticket = _ticketServices.CreateTicket(model);
            return RedirectToAction("Index", "Ticket");
        }

        public ActionResult Edit(Guid Id)
        {
            TicketViewModel ticketViewModel = _ticketServices.GetTicket(Id);
            ticketViewModel.PriorityDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Priority);
            ticketViewModel.StatusDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Status);
            ticketViewModel.TypeDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Type);
            ticketViewModel.AssignedDropDown = _userServices.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
            return View(ticketViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TicketViewModel model, HttpPostedFileBase file,string ticketAttachmentIds)
        {
            if (file != null)
            {
                model.Image = model.Id + "_" + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//TicketAttachment//") + model.Image);

                model.PriorityDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Priority);
                model.StatusDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Status);
                model.TypeDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Type);
                model.AssignedDropDown = _userServices.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();
               
            }
            var ticketdeleteid = Request.Params["hdnAttachmentDeleteId"];
            Ticket ticket = _ticketServices.UpdateTicket(model, ticketdeleteid);
            return RedirectToAction("Index", "Ticket");
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid Id)
        {
            _ticketServices.RemoveTicket(Id);
            return RedirectToAction("Index","Ticket");
        }


        //Ticket kendo json
        public ActionResult GetAllTicketJson([DataSourceRequest] DataSourceRequest request)
        {
            List<TicketViewModel> ticketViewModels = _ticketServices.GetTicketList().ToList();
            return Json(ticketViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        //for statusfilter(dropdown)
        public ActionResult StatusFilter()
        {
            var statusFilter = _ticketServices.SetDropDownValue(Constant.ConfigName.Status);
            return Json(statusFilter, JsonRequestBehavior.AllowGet);
        }
    }
}