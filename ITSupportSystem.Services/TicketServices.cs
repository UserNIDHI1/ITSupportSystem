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
        Ticket UpdateTicket(TicketViewModel model);
        List<TicketViewModel> GetTicketList();
        TicketViewModel GetTicket(Guid Id);

        List<DropDown> SetDropDownValue(string configName);
    }

    public class TicketServices : ITicketServices
    {

        ITicketRepository _ticketRepository;
        ICommonLookUpServices _commonLookUpServices;
        IRepository<TicketAttachment> _ticketAttachmentRepository;

        public TicketServices(ITicketRepository ticketRepository, ICommonLookUpServices commonLookUpServices, IRepository<TicketAttachment> ticketAttachmentRepository)
        {
            _ticketRepository = ticketRepository;
            _commonLookUpServices = commonLookUpServices;
            _ticketAttachmentRepository = ticketAttachmentRepository;
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

            _ticketRepository.Insert(ticket);
            _ticketRepository.commit();

            if(model.Image!=null)
            {
                TicketAttachment ticketAttachment = new TicketAttachment();
                {
                    ticketAttachment.TicketId = ticket.Id;
                    ticketAttachment.FileName = model.Image;
                }
                _ticketAttachmentRepository.Insert(ticketAttachment);
                _ticketAttachmentRepository.commit();
            }
            return ticket;
        }
        public Ticket UpdateTicket(TicketViewModel model)

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

            if(model.Image!=null)
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
            return ticket;
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


    }
}
