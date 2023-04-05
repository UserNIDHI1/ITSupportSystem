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
        string CreateRole(RoleViewModel role);
        List<Role> GetRoleList();
        RoleViewModel GetRole(Guid Id);
        string UpdateRole(RoleViewModel model);
        void RemoveRole(RoleViewModel model);

    }

    public class RoleServices : IRoleServices
    {
        IRoleRepository roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public string CreateRole(RoleViewModel role)
        {
            if (roleRepository.Collection().Where(u => u.Name == role.Name).Any())
            {
                return "Name is already exist";
            }
            if (roleRepository.Collection().Where(u => u.Code == role.Code).Any())
            {
                return "Code is already exist";
            }
            Role roleData = new Role();
            roleData.Name = role.Name;
            roleData.Code = role.Code.ToUpper();
            roleRepository.Insert(roleData);
            roleRepository.commit();

            return null;
        }

        public RoleViewModel GetRole(Guid Id)
        {
            Role role = roleRepository.Find(Id);
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Id = role.Id;
            roleViewModel.Name = role.Name;
            roleViewModel.Code = role.Code;
            return roleViewModel;
        }

        public List<Role> GetRoleList()
        {
            return roleRepository.Collection().Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedOn).ToList();
        }

        public void RemoveRole(RoleViewModel model)
        {
            Role role = roleRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            role.IsDeleted = true;
            roleRepository.Update(role);
            roleRepository.commit();
        }

        public string UpdateRole(RoleViewModel model)
        {
            if (roleRepository.Collection().Where(r => r.Id != model.Id && r.Name == model.Name).Any())
            {
                return "Name is already exist";
            }
            if (roleRepository.Collection().Where(r => r.Id != model.Id && r.Code == model.Code).Any())
            {
                return "Code is already exist";
            }


            Role role = roleRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            role.Name = model.Name;
            role.Code = model.Code.ToUpper();
            role.UpdatedOn = DateTime.Now;
            roleRepository.Update(role);
            roleRepository.commit();
            return null;
        }
    }
}




