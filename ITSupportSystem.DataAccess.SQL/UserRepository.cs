using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.DataAccess.SQL.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{
    public class UserRepository : IUserRepository
    {
        internal DataContext contex;
        internal DbSet<Users> dbSet;

        public UserRepository()
        {
            contex = new DataContext();
            this.dbSet = contex.Set<Users>();
        }

        public IQueryable<Users> Collection()
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

        public Users Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(Users user)
        {
            dbSet.Add(user);
        }

        public void Update(Users user)
        {
            dbSet.Attach(user);
            contex.Entry(user).State = EntityState.Modified;
        }
    }
}
