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
    public interface IFormRepository
    {
        IQueryable<Form> Collection();
        void commit();
        void Insert(Form form);
        void Update(Form form);
        Form Find(Guid Id);
        void Delete(Guid Id);
        List<FormViewModel> GetFormList();
        FormViewModel GetformById(Guid Id);
    }


    public class FormRepository : IFormRepository
    {

        internal DataContext contex;
        internal DbSet<Form> dbSet;

        public FormRepository()
        {
            contex = new DataContext();
            this.dbSet = contex.Set<Form>();
        }

        public IQueryable<Form> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            contex.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var t = Find(Id);
            if (contex.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public Form Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public List<FormViewModel> GetFormList()
        {
            var formlist = (from f in contex.Form.Where(x => !x.IsDeleted).AsEnumerable()
                            join p in contex.Form on f.ParentFormId equals p.Id into fdata
                            from pf in fdata.DefaultIfEmpty()
                            select new FormViewModel()
                            {
                                Id = f.Id,
                                ParentFormId = pf?.ParentFormId,
                                ParentFormName = pf?.Name,
                                FormAccessCode = f.FormAccessCode,
                                DisplayIndex = f.DisplayIndex,
                                NavigateURL = f.NavigateURL,
                                Name = f.Name
                            }
                         ).Distinct().ToList();
            return formlist;
        }

        public FormViewModel GetformById(Guid Id)
        {
            var form = (from f in contex.Form
                        join p in contex.Form on f.ParentFormId equals p.Id into fdata
                        from pf in fdata.DefaultIfEmpty()
                        where !f.IsDeleted && f.Id == Id
                        select new FormViewModel()
                        {
                            Id = f.Id,
                            ParentFormId = f.ParentFormId,
                            ParentFormName = pf.Name,
                            FormAccessCode = f.FormAccessCode,
                            DisplayIndex = f.DisplayIndex,
                            NavigateURL = f.NavigateURL,
                            Name = f.Name
                        }).FirstOrDefault();
            return form;
        }

        public void Insert(Form form)
        {
            dbSet.Add(form);
        }

        public void Update(Form form)
        {
            dbSet.Attach(form);
            contex.Entry(form).State = EntityState.Modified;
        }
    }
}
