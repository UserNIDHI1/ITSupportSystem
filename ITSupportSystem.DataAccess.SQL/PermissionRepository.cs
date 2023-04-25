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
        void UpdatePermission(Permission permission);
        void Delete(Guid Id);
        List<PermissionViewModel> GetPermission(Guid RoleId);
        void InsertRange(List<Permission> model);
    }

    public class PermissionRepository : IPermissionRepository
    {
        internal DataContext context;
        internal DbSet<Permission> dbSet;

        public PermissionRepository()
        {
            context = new DataContext();
            this.dbSet = context.Set<Permission>();
        }

        public IQueryable<Permission> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Permission Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void InsertRange(List<Permission> model)
        {
            var recordstoDelete = Collection().ToList().Where(x => x.RoleId == model.FirstOrDefault().RoleId);
            dbSet.RemoveRange(recordstoDelete);
            commit();
            dbSet.AddRange(model);
            commit();
        }

        public void UpdatePermission(Permission permission)
        {
            dbSet.Attach(permission);
            context.Entry(permission).State = EntityState.Modified;
        }

        public List<PermissionViewModel> GetPermission(Guid RoleId)
        {
            var PermissionsList = (from f in context.Form.Where(x => !x.IsDeleted).AsEnumerable()
                                   join p in context.Permission.Where(x => x.RoleId == RoleId) on
                                   f.Id equals p.FormId into fp
                                   from fPer in fp.DefaultIfEmpty()
                                   select new PermissionViewModel
                                   {
                                       FormId = f.Id,
                                       FormName = f.Name,
                                       RoleId = RoleId,
                                       NavigateUrl = f.NavigateURL,
                                       ParentFormId=f.ParentFormId,
                                       View = fPer != null ? fPer.View : false,
                                       Insert = fPer != null ? fPer.Insert : false,
                                       Update = fPer != null ? fPer.Update : false,
                                       Delete = fPer != null ? fPer.Delete : false
                                   }).ToList();
            return PermissionsList;
        }
    }
}
