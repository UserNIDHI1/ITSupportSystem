using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Contracts
{
    public interface IUserRepository
    {
        IQueryable<Users> Collection();
        void commit();
        void Insert(Users user);
        void Update(Users user);
        Users Find(Guid Id);
        void Delete(Guid Id);
        List<UserViewModel> GetUserList();
        UserViewModel GetUser(Guid Id);
    }
}
