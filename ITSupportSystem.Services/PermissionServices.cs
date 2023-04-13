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
    public interface IPermissionServices
    {
        List<PermissionViewModel> GetPermissionn(Guid Id);
    }

    public class PermissionServices : IPermissionServices
    {
        IPermissionRepository _permissionRepository;
        IFormServices _formServices;

        public PermissionServices(IPermissionRepository permissionRepository, IFormServices formServices)
        {
            this._permissionRepository = permissionRepository;
            this._formServices = formServices;
        }

        public List<PermissionViewModel> GetPermissionn(Guid RoleId)
        {
            return _permissionRepository.GetPermission(RoleId).ToList();
        }

    }
}

