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
        List<PermissionViewModel> GetPermission(Guid RoleId);
        void UpdatePermission(List<Permission> model);
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

        public List<PermissionViewModel> GetPermission(Guid RoleId)
        {
            return _permissionRepository.GetPermission(RoleId).ToList();
        }

        public void UpdatePermission(List<Permission> model)
        {
            _permissionRepository.InsertRange(model);
        }
    }
}

