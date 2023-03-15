//using ITSupportSystem.Core1.Contract;
//using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return loginRepository.Login(model);

        }
    }
}
