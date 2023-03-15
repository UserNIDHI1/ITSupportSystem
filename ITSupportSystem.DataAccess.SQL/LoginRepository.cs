using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ITSupportSystem.DataAccess.SQL
{
    public class LoginRepository : ILoginRepository
    {
        internal DataContext context;

        public LoginRepository(DataContext context)
        {
            this.context = context;
        }
        public Users Login(LoginViewModel model)
        {
            var user = context.User.Where(Users => Users.Email == model.Email && Users.Password == model.Password).FirstOrDefault();
            return user;
        }



































        //internal DataContext context;
        //internal DbSet dbSet;

        //public LoginRepository()
        //{
        //    context = new DataContext();
        //}

        //public void commit()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Email(Guid Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Users Login(LoginViewModel model)
        //{
        //    var users = context.User.Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();
        //    return users;
        //}

        //public void save()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
