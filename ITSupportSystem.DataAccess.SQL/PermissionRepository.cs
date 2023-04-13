using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{
    public interface IPermissionRepository
    {
        IQueryable<Permission> Collection();
        void commit();
        Permission Find(Guid Id);
        void Insert(Permission permission);
        void Update(Permission permission);
        void Delete(Guid Id);
        List<PermissionViewModel> GetPermission(Guid RoleId);
    }

    public class PermissionRepository : IPermissionRepository
    {
        internal DataContext contex;
        internal DbSet<Permission> dbSet;

        public PermissionRepository()
        {
            contex = new DataContext();
            this.dbSet = contex.Set<Permission>();
        }

        public IQueryable<Permission> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            contex.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Permission Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(Permission permission)
        {
            dbSet.Add(permission);
        }

        public void Update(Permission permission)
        {
            dbSet.Attach(permission);
            contex.Entry(permission).State = EntityState.Modified;
        }

        public List<PermissionViewModel> GetPermission(Guid RoleId)
        {
            var permission = contex.Permission.Where(x => x.RoleId == RoleId).FirstOrDefault();
            if (permission == null)
            {
                var permissions = (from f in contex.Form
                                   where !f.IsDeleted
                                   orderby f.Name ascending
                                   select new PermissionViewModel
                                   {
                                       FormId = f.Id,
                                       FormName = f.Name,
                                       RoleId = RoleId,
                                       View = false,
                                       Insert = false,
                                       Update = false,
                                       Delete = false
                                   }).ToList();
                return permissions;

            }
            else
            {
                var permissions = (from f in contex.Form
                                   join p in contex.Permission on f.Id equals p.FormId
                                   where !f.IsDeleted && p.RoleId == RoleId
                                   orderby f.Name ascending
                                   select new PermissionViewModel
                                   {
                                       FormId = f.Id,
                                       FormName = f.Name,
                                       RoleId = RoleId,
                                       View = p.View,
                                       Insert = p.Insert,
                                       Update = p.Update,
                                       Delete = p.Delete
                                   }).ToList();


                var forms = (from f in contex.Form
                             where !f.IsDeleted
                             orderby f.Name ascending
                             select new PermissionViewModel
                             {
                                 FormId = f.Id,
                                 FormName = f.Name,
                                 RoleId = RoleId,
                                 View = false,
                                 Insert = false,
                                 Update = false,
                                 Delete = false
                             }).ToList();

                var data = forms.Except(permissions).ToList();
                var res = permissions.Union(data).ToList();
                return res;
            }
        }
    }
}
