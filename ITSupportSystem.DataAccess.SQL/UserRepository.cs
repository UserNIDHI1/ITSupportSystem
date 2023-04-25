using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
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
        internal DbSet<UserRole> userroledbSet;

        public UserRepository()
        {
            contex = new DataContext();
            this.dbSet = contex.Set<Users>();
            this.userroledbSet = contex.Set<UserRole>();
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
            Users t = Find(Id);
            if (contex.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public Users Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public UserViewModel GetUser(Guid Id)
        {
            var list = (from u in contex.User
                        join ur in contex.UserRole on u.Id equals ur.UserId
                        where !u.IsDeleted && !ur.IsDeleted && u.Id == Id
                        orderby u.CreatedOn descending
                        select new UserViewModel()
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Email = u.Email,
                            RoleId = ur.RoleId,
                            Password = u.Password,
                            UserName = u.UserName,
                            Gender = u.Gender,
                            MobileNo = u.MobileNo
                        }
                        ).FirstOrDefault();
            return list;
        }

        public List<UserViewModel> GetUserList()
        {
            var result = (from u in contex.User
                          join ur in contex.UserRole on u.Id equals ur.UserId
                          join r in contex.Role on ur.RoleId equals r.Id
                          where !u.IsDeleted && !ur.IsDeleted && !r.IsDeleted
                          orderby u.CreatedOn descending
                          select new UserViewModel()
                          {
                              Id = u.Id,
                              Name = u.Name,
                              Email = u.Email,
                              Password = u.Password,
                              RoleId = ur.RoleId,
                              RoleName = r.Name,
                              UserName = u.UserName,
                              Gender = u.Gender,
                              MobileNo = u.MobileNo
                          }
                        ).ToList();
            return result;
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
