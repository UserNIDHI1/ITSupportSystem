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
    public interface IUserServices
    {
        void CreateUser(UserViewModel user);
        List<Users> GetUserList();
        UserViewModel GetUser(Guid Id);
        void UpdateUser(UserViewModel model);
        void RemoveUser(UserViewModel model);
    }
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateUser(UserViewModel user)
        {
            Users userData = new Users();
            userData.Name = user.Name;
            userData.Email = user.Email;
            userData.Password=user.Password;
            userRepository.Insert(userData);
            userRepository.commit();
        }

        public UserViewModel GetUser(Guid Id)
        {
            Users user = userRepository.Find(Id);
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = user.Id;
            userViewModel.Name = user.Name;
            userViewModel.Email = user.Email;
            userViewModel.Password = user.Password;
            return userViewModel;
        }

        public List<Users> GetUserList()
        {
            //var query = from Users in userRepository.Collection()
            //            select Users;
            //var content = query.ToList<Users>();
            //return content;
            return userRepository.Collection().Where(x => !x.IsDeleted).ToList();
        }

        public void RemoveUser(UserViewModel model)
        {
            Users user = userRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            user.IsDeleted = true;
            userRepository.Update(user);
            userRepository.commit();
        }
        

        public void UpdateUser(UserViewModel model)
        {
            Users user = userRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;
            user.UpdatedOn = DateTime.Now;
            userRepository.Update(user);
            userRepository.commit();
        }
    }
}


