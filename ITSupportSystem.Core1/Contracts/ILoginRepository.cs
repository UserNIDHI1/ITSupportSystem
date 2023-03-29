using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public interface ILoginRepository
    {
        Users Login(LoginViewModel model);
    }
}





