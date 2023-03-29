﻿using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Services
{
    public interface IUserServices
    {
        string CreateUser(UserViewModel user);
        List<UserViewModel> GetUserList();
        UserViewModel GetUser(Guid Id);
        string UpdateUser(UserViewModel model);
        void RemoveUser(Guid Id);
        Guid GetRoleIdByUserId(Guid Id);
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

        public string CreateUser(UserViewModel user)
        {
            if (userRepository.Collection().Where(u => u.UserName == user.UserName).Any())
            {
                return "UserName is already exist";
            }
            if (userRepository.Collection().Where(u => u.Email == user.Email).Any())
            {
                return "Email is already exist";
            }


            Users userData = new Users();
            string salt = "";
            string password = HashPasword(user.Password, out salt);

            userData.Id = user.Id;
            userData.Name = user.Name;
            userData.Email = user.Email;
            userData.Password = password;
            userData.PasswordSalt = salt;
            userData.UserName = user.UserName;
            userData.Gender = user.Gender;
            userData.MobileNo = user.MobileNo;

            UserRole userRole = new UserRole();
            userRole.RoleId = user.RoleId;
            userRole.UserId = user.Id;
            userRepository.Insert(userData);
            _userrolerepository.Insert(userRole);
            userRepository.commit();

            return null;
        }

        public string UpdateUser(UserViewModel model)
        {
            if (userRepository.Collection().Where(u => u.Id != model.Id && u.UserName == model.UserName).Any())
            {
                return "UserName is already exist";
            }
            if (userRepository.Collection().Where(u => u.Id != model.Id && u.Email == model.Email).Any())
            {
                return "Email is already exist";
            }

            Users user = userRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            user.Name = model.Name;
            user.Email = model.Email;
            user.Password = model.Password;
            user.UserName = model.UserName;
            user.Gender = model.Gender;
            user.MobileNo = model.MobileNo;
            user.UpdatedOn = DateTime.Now;
            userRepository.Update(user);
            userRepository.commit();

            UserRole userrole = _userrolerepository.Collection().Where(x => x.UserId == model.Id).FirstOrDefault();
            userrole.RoleId = model.RoleId;
            userrole.UpdatedOn = DateTime.Now;
            _userrolerepository.Update(userrole);
            _userrolerepository.Commit();

            return null;
        }

        public Guid GetRoleIdByUserId(Guid Id)
        {
            Guid roleId = _userrolerepository.Collection().Where(x => x.UserId == Id).Select(x => x.RoleId).FirstOrDefault();
            return roleId;
        }

        public UserViewModel GetUser(Guid Id)
        {
            Users user = userRepository.Find(Id);
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = user.Id;
            userViewModel.Name = user.Name;
            userViewModel.Email = user.Email;
            userViewModel.Password = user.Password;
            userViewModel.UserName = user.UserName;
            userViewModel.Gender = user.Gender;
            userViewModel.MobileNo = user.MobileNo;
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

        private static string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private string HashPasword(string Password, out string salt)
        {
            salt = CreateSalt(64);
            string stringDataToHash = Password + "" + salt;
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(stringDataToHash);
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            string base64 = Convert.ToBase64String(bytHash);
            return base64;
        }

    }
}
