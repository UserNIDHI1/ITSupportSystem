using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{
    public class RoleRepository : IRoleRepository
    {
        internal DataContext contex;
        internal DbSet<Role> dbSet;

        public RoleRepository()
        {
            contex = new DataContext();
            this.dbSet = contex.Set<Role>();
        }

        public IQueryable<Role> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            contex.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (contex.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
            
        }

        public Role Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(Role role)
        {
            dbSet.Add(role);
        }

        public void Update(Role role)
        {
            dbSet.Attach(role);
            contex.Entry(role).State = EntityState.Modified;
        }
    }
}
