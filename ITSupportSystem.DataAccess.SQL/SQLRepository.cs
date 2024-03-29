﻿using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbSet;
        internal DbSet<TicketAttachment> ticketAttachments;
        internal DbSet<TicketComment> ticketComment;


        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this.ticketAttachments = context.Set<TicketAttachment>();
            this.ticketComment = context.Set<TicketComment>();
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
           
        }
    }
}

