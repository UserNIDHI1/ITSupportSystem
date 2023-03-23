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
        List<UserViewModel> GetUserList();
        UserViewModel GetUser(Guid Id);
        void UpdateUser(UserViewModel model);
        void RemoveUser(Guid Id);

    }
    public class UserServices : IUserServices
    {
        IUserRepository userRepository;
        IRepository<UserRole> _userrolerepository;

        public UserServices(IUserRepository userRepository, IRepository<UserRole> userrolerepository)
        {
            this.userRepository = userRepository;
            this._userrolerepository = userrolerepository;
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

        public void UpdateUser(UserViewModel model)
        {
            Users user = userRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            user.Name = model.Name;
            user.Email = model.Email;
            user.UpdatedOn = DateTime.Now;
            userRepository.Update(user);
            userRepository.commit();

            UserRole userrole = _userrolerepository.Collection().Where(x => x.UserId == model.Id).FirstOrDefault();
            userrole.RoleId = model.RoleId;
            userrole.UpdatedOn = DateTime.Now;
            _userrolerepository.Update(userrole);
            _userrolerepository.Commit();
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

        public List<UserViewModel> GetUserList()
        {
            return userRepository.GetUserList();
        }

        public void RemoveUser(Guid Id)
        {
            Users user = userRepository.Collection().Where(x => x.Id == Id).FirstOrDefault();
            user.IsDeleted = true;
            userRepository.Update(user);
            userRepository.commit();

            UserRole userRole = _userrolerepository.Collection().Where(x => x.UserId == Id).FirstOrDefault();
            userRole.IsDeleted = true;
            _userrolerepository.Update(userRole);
            _userrolerepository.Commit();

        }
    }
}


