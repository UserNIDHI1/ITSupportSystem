using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Services
{
    public interface ITicketServices
    {
        Ticket CreateTicket(TicketViewModel model);
        Ticket UpdateTicket(TicketViewModel model,string ticketIds);
        Ticket RemoveTicket(Guid Id);
        TicketComment RemoveTicketComment(TicketCommentViewModel model);
        void EditTicketComment(TicketCommentViewModel model);
        List<TicketViewModel> GetTicketList();
        TicketViewModel GetTicket(Guid Id);
        List<DropDown> SetDropDownValue(string configName);
        TicketComment CommentTicket(TicketCommentViewModel model);

        List<TicketCommentViewModel> GetCommentList(Guid Id);
        TicketCommentViewModel GetCommentListEdit(Guid Id);


    }

    public class TicketServices : ITicketServices
    {
        ITicketRepository _ticketRepository;
        ICommonLookUpServices _commonLookUpServices;
        IRepository<TicketAttachment> _ticketAttachmentRepository;
        IRepository<TicketComment> _ticketCommentRepository;

        public TicketServices(ITicketRepository ticketRepository, ICommonLookUpServices commonLookUpServices, IRepository<TicketAttachment> ticketAttachmentRepository, IRepository<TicketComment> ticketCommentRepository)
        {
            _ticketRepository = ticketRepository;
            _commonLookUpServices = commonLookUpServices;
            _ticketAttachmentRepository = ticketAttachmentRepository;
            _ticketCommentRepository = ticketCommentRepository;
        }
        public Ticket CreateTicket(TicketViewModel model)
        {
            Ticket ticket = new Ticket();
            ticket.Title = model.Title;
            ticket.Description = model.Description;
            ticket.AssignTo = model.AssignTo;
            ticket.PriorityId = model.PriorityId;
            ticket.StatusId = model.StatusId;
            ticket.TypeId = model.TypeId;
            ticket.Id = model.Id;
            ticket.CreatedBy = model.CreatedBy;
            _ticketRepository.Insert(ticket);
            _ticketRepository.commit();

            if (model.Image != null)
            {
                TicketAttachment ticketAttachment = new TicketAttachment();
                {
                    ticketAttachment.TicketId = ticket.Id;
                    ticketAttachment.FileName = model.Image;
                    ticketAttachment.CreatedBy = model.CreatedBy;
                }
                _ticketAttachmentRepository.Insert(ticketAttachment);
                _ticketAttachmentRepository.commit();
            }
            return ticket;
        }
        public Ticket UpdateTicket(TicketViewModel model, string ticketIds)
        {
            Ticket ticket = _ticketRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            ticket.Title = model.Title;
            ticket.Description = model.Description;
            ticket.AssignTo = model.AssignTo;
            ticket.PriorityId = model.PriorityId;
            ticket.StatusId = model.StatusId;
            ticket.TypeId = model.TypeId;
            ticket.Id = model.Id;
            ticket.UpdatedOn = DateTime.Now;
            _ticketRepository.Update(ticket);
            _ticketRepository.commit();

            if (model.Image != null)
            {
                TicketAttachment ticketAttachment = new TicketAttachment();
                {
                    ticketAttachment.TicketId = ticket.Id;
                    ticketAttachment.FileName = model.Image;
                    ticketAttachment.Id = Guid.NewGuid();
                }
                _ticketAttachmentRepository.Insert(ticketAttachment);
                _ticketAttachmentRepository.commit();
            }


            if(!string.IsNullOrEmpty(ticketIds))
            {
                string[] ticketAttachment = ticketIds.Split(',');
                if(ticketAttachment != null && ticketAttachment.Length>0)
                {
                    foreach(var item in ticketAttachment)
                    {
                        var TicketToDelete = _ticketAttachmentRepository.Collection().Where(x => x.Id.ToString() == item).FirstOrDefault();
                        TicketToDelete.IsDeleted = true;
                        _ticketAttachmentRepository.Update(TicketToDelete);
                        _ticketAttachmentRepository.commit();
                    }
                }
            }
            return ticket;
        }

        public Ticket RemoveTicket(Guid Id)
        {
            TicketViewModel model = new TicketViewModel();
            Ticket ticket = _ticketRepository.Collection().Where(x => x.Id == Id).FirstOrDefault();
            ticket.IsDeleted = true;
            _ticketRepository.Update(ticket);
            _ticketRepository.commit();
            return ticket;
        }

        public TicketComment RemoveTicketComment(TicketCommentViewModel model)
        {
            TicketComment ticketComment = _ticketCommentRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            ticketComment.IsDeleted = true;
            _ticketCommentRepository.Update(ticketComment);
            _ticketCommentRepository.commit();
            return ticketComment;
        }


        public TicketViewModel GetTicket(Guid Id)
        {
            return _ticketRepository.Getticket(Id);
        }

        public List<TicketViewModel> GetTicketList()
        {
            return _ticketRepository.GetTicketList().OrderByDescending(x => x.CreatedOn).ToList();
        }

        //for get config key from the commonllokup
        public List<DropDown> SetDropDownValue(string configName)
        {
            return _commonLookUpServices.CommonLookUpByName(configName).Select(x => new DropDown { Id = x.Id, Name = x.ConfigKey }).ToList();
        }

        public TicketComment CommentTicket(TicketCommentViewModel model)
        {
            TicketComment ticketComment = new TicketComment();
            ticketComment.Comment = model.Comment;
            ticketComment.TicketId = model.TicketId;
            ticketComment.CreatedBy = model.CreatedBy;
            ticketComment.CreatedOn = DateTime.Now;
            _ticketCommentRepository.Insert(ticketComment);
            _ticketCommentRepository.commit();
            return ticketComment;
        }

        public List<TicketCommentViewModel> GetCommentList(Guid Id)
        {
            return _ticketRepository.GetTicketCommentList(Id);
        }

        public TicketCommentViewModel GetCommentListEdit(Guid Id)
        {
            return _ticketRepository.GetTicketCommentById(Id);
        }

        public void EditTicketComment(TicketCommentViewModel model)
        {
            TicketComment ticketComment = _ticketCommentRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            ticketComment.Comment = model.Comment;
            ticketComment.UpdatedOn = DateTime.Now;
            _ticketCommentRepository.Update(ticketComment);
            _ticketCommentRepository.commit();
        }
    }
}
