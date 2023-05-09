using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Collection();
        void commit();
        void Insert(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(Guid Id);
        Ticket Find(Guid Id);
        List<TicketViewModel> GetTicketList();
        TicketViewModel Getticket(Guid Id);
    }


    public class TicketRepository : ITicketRepository
    {
        internal DataContext context;
        internal DbSet<Ticket> dbSet;      

        public TicketRepository()
        {
            context = new DataContext();
            this.dbSet = context.Set<Ticket>();           
        }

        public IQueryable<Ticket> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            context.SaveChanges();
        }
        public void Insert(Ticket ticket)
        {
            dbSet.Add(ticket);
        }

        public void Update(Ticket ticket)
        {
            dbSet.Attach(ticket);
            context.Entry(ticket).State = EntityState.Modified;
            commit();
        }
        public Ticket Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        //Edit case
        public TicketViewModel Getticket(Guid Id)
        {
            var ticket = (from t in context.Ticket
                          join u in context.User on t.AssignTo equals u.Id
                          join ct in context.CommonLookUp on t.TypeId equals ct.Id
                          join cp in context.CommonLookUp on t.PriorityId equals cp.Id
                          join cs in context.CommonLookUp on t.StatusId equals cs.Id
                          where !t.IsDeleted && t.Id == Id
                          select new TicketViewModel()
                          {
                              Id = t.Id,
                              Title = t.Title,
                              Description = t.Description,
                              AssignTo = u.Id,
                              TypeId = ct.Id,
                              PriorityId = cp.Id,
                              StatusId = cs.Id,
                              Assigned = u.Name,
                              Type = ct.ConfigKey,
                              Priority = cp.ConfigKey,
                              Status = cs.ConfigKey,
                          }).AsEnumerable();

            var tickets = (from t in ticket
                           join ta in context.TicketAttachment.Where(x=>!x.IsDeleted) on t.Id equals ta.TicketId
                           into fdata
                           from fd in fdata.DefaultIfEmpty()
                           group fd by t into g
                           select new TicketViewModel
                           {
                               Id = g.Key.Id,
                               Title = g.Key.Title,
                               Description = g.Key.Description,
                               AssignTo = g.Key.AssignTo,
                               TypeId = g.Key.TypeId,
                               PriorityId = g.Key.PriorityId,
                               StatusId = g.Key.StatusId,
                               Assigned = g.Key.Assigned,
                               Type = g.Key.Type,
                               Priority = g.Key.Priority,
                               Status = g.Key.Status,
                               TicketAttachment =g.Where(x =>x != null && x.FileName!=null).Any() ? g.ToList() : null,
                               AttachmentCount = g.Where(x => x != null && x.FileName != null).Any() ? g.Count() : 0
                           }).FirstOrDefault();
            return tickets;
        }

        //GetTicketList
        public List<TicketViewModel> GetTicketList()
        {
            var ticketList = (from t in context.Ticket
                              join u in context.User on t.AssignTo equals u.Id
                              join ct in context.CommonLookUp on t.TypeId equals ct.Id
                              join cp in context.CommonLookUp on t.PriorityId equals cp.Id
                              join cs in context.CommonLookUp on t.StatusId equals cs.Id
                              where !t.IsDeleted
                              select new TicketViewModel()
                              {
                                  Id = t.Id,
                                  Title = t.Title,
                                  Description = t.Description,
                                  AssignTo = u.Id,
                                  TypeId = ct.Id,
                                  PriorityId = cp.Id,
                                  StatusId = cs.Id,
                                  Assigned = u.Name,
                                  Type = ct.ConfigKey,
                                  Priority = cp.ConfigKey,
                                  Status = cs.ConfigKey,
                              }).AsEnumerable();
            var ticket = (from t in ticketList
                          join ta in context.TicketAttachment.Where(x => !x.IsDeleted) on t.Id equals ta.TicketId
                          into fdata
                          from fd in fdata.DefaultIfEmpty()
                          group fd by t into g
                          select new TicketViewModel
                          {
                              Id = g.Key.Id,
                              Title = g.Key.Title,
                              Description = g.Key.Description,
                              AssignTo = g.Key.AssignTo,
                              TypeId = g.Key.TypeId,
                              PriorityId = g.Key.PriorityId,
                              StatusId = g.Key.StatusId,
                              Assigned = g.Key.Assigned,
                              Type = g.Key.Type,
                              Priority = g.Key.Priority,
                              Status = g.Key.Status,
                              TicketAttachment = g.Where(x => x != null && x.FileName != null).Any() ? g.ToList() : null,
                              AttachmentCount = g.Where(x => x != null && x.FileName != null).Any() ? g.Count() : 0
                          }).ToList();
            return ticket;
        }
    }
}
