using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Services
{
    public interface ILoginService
    {
        Users Login(LoginViewModel model);
    }

    public class LoginServices : ILoginService
    {
        ILoginRepository loginRepository;
        public LoginServices(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        public Users Login(LoginViewModel model)
        {
            Users user = loginRepository.Login(model);
            if(user!=null)
            {
                string hash = HashPasword(model.Password, user.PasswordSalt);
                if (hash.SequenceEqual(user.Password))
                {
                    return user;
                }
            }
            return null;
        }
        private string HashPasword(string Password, string salt)
        {
            string stringDataToHash = Password + "" + salt;
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(stringDataToHash);
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            string base64 = Convert.ToBase64String(bytHash);
            return base64;
        }

    }
}
