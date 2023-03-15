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
    public interface IRoleServices
    { 
        void CreateRole(RoleViewModel role);
        List<Role> GetRoleList();
        RoleViewModel GetRole(Guid Id);
        void UpdateRole(RoleViewModel model);
        void RemoveRole(RoleViewModel model);
    }
    public class RoleServices : IRoleServices
    {
        IRoleRepository roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public void CreateRole(RoleViewModel role)
        {
            Role roleData = new Role();
            roleData.Name = role.Name;
            roleData.Code = role.Code;
            roleRepository.Insert(roleData);
            roleRepository.commit();
        }

        public RoleViewModel GetRole(Guid Id)
        {
            var role = roleRepository.Find(Id);
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Id = role.Id;
            roleViewModel.Name = role.Name;
            roleViewModel.Code = role.Code;
            return roleViewModel;
        }

        public List<Role> GetRoleList()
        {
            return roleRepository.Collection().Where(x => !x.IsDeleted).ToList();
        }

        public void RemoveRole(RoleViewModel model)
        {
            var role = roleRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            role.IsDeleted = true;
            roleRepository.Update(role);
            roleRepository.commit();
        }

        public void UpdateRole(RoleViewModel model)
        {
            var role = roleRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            role.Name = model.Name;
            role.Code = model.Code;
            role.UpdatedOn = DateTime.Now;
            roleRepository.Update(role);
            roleRepository.commit();
        }
    }
}




