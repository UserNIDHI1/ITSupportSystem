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

            }
            model.CreatedBy = new Guid(Session["Id"].ToString());
            model.PriorityDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Priority);
            model.StatusDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Status);
            model.TypeDropDown = _ticketServices.SetDropDownValue(Constant.ConfigName.Type);
            model.AssignedDropDown = _userServices.GetUserList().Select(x => new DropDown() { Id = x.Id, Name = x.Name }).ToList();


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
        public ActionResult Edit(TicketViewModel model, HttpPostedFileBase file, string ticketAttachmentIds)
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
            return RedirectToAction("Index", "Ticket");
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

        public ActionResult Details(Guid Id)
        {
            TicketViewModel ticketViewModel = _ticketServices.GetTicket(Id);
            return View(ticketViewModel);
        }


        public ActionResult Comment(TicketCommentViewModel model)
        {
            model.CreatedBy = (Guid)Session["Id"];
            _ticketServices.CommentTicket(model);
            return Content("true");
        }

        //create ticket comment
        public ActionResult CommentIndexList(Guid Id)
        {
            List<TicketCommentViewModel> ticketCommentViewModel = _ticketServices.GetCommentList(Id).ToList();
            return PartialView("CommentListPartialView", ticketCommentViewModel);
        }


        public ActionResult CommentIndexListEdit(Guid Id)
        {
            TicketCommentViewModel ticketCommentModel = _ticketServices.GetCommentListEdit(Id);
            return View("CommentListPartialView", ticketCommentModel);
        }

        [HttpPost]
        public ActionResult DeleteComment(Guid Id)
        {
            var commit = _ticketServices.GetCommentListEdit(Id);
            _ticketServices.RemoveTicketComment(commit);
            return Content("true");
        }

        public ActionResult CommentEdit(TicketCommentViewModel model)
        {
            _ticketServices.EditTicketComment(model);
            return Content("true");
        }
    }
}