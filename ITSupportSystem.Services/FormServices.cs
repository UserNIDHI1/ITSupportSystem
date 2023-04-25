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
    public interface IFormServices
    {
        string CreateForm(FormViewModel model);
        List<FormViewModel> GetFormList();
        FormViewModel GetForm(Guid Id);
        string UpdateForm(FormViewModel model);
        void RemoveForm(Guid Id);
    }

    public class FormServices : IFormServices
    {
        IFormRepository _formRepository;
        IPermissionRepository _permissionRepository;

        public FormServices(IFormRepository formRepository,IPermissionRepository permissionRepository)
        {
            _formRepository = formRepository;
            _permissionRepository = permissionRepository;
        }

        public string CreateForm(FormViewModel model)
        {
            if (_formRepository.Collection().Where(f => f.FormAccessCode == model.FormAccessCode).Any())
            {
                return "FormAccessCode is already exist";
            }
            if (_formRepository.Collection().Where(f => f.Name == model.Name).Any())
            {
                return "Name is already exist";
            }
            Form formData = new Form();
            formData.Name = model.Name;
            formData.NavigateURL = model.NavigateURL;
            formData.FormAccessCode = model.FormAccessCode;
            formData.DisplayIndex = model.DisplayIndex;
           if(model.ParentFormId!=null)
            {
                formData.ParentFormId = model.ParentFormId;
            }
            _formRepository.Insert(formData);
            _formRepository.commit();
            return null;
        }

        public FormViewModel GetForm(Guid Id)
        {
            return _formRepository.Getform(Id);
        }
        public List<FormViewModel> GetFormList()
        {
            return _formRepository.GetFormList().OrderByDescending(x => x.CreatedOn).ToList();
        }

        public void RemoveForm(Guid Id)
        {
            Form form = _formRepository.Collection().Where(x => x.Id == Id).FirstOrDefault();
            form.IsDeleted = true;
            _formRepository.Update(form);
            _formRepository.commit();
        }

        public string UpdateForm(FormViewModel model)
        {
            if (_formRepository.Collection().Where(f => f.Id != model.Id && f.FormAccessCode == model.FormAccessCode).Any())
            {
                return "FormAccessCode is already exist";
            }
            if (_formRepository.Collection().Where(f => f.Id != model.Id && f.Name == model.Name).Any())
            {
                return "Name is already exist";
            }
            Form form = _formRepository.Collection().Where(x => x.Id == model.Id).FirstOrDefault();
            form.Name = model.Name;
            form.NavigateURL = model.NavigateURL;
            form.FormAccessCode = model.FormAccessCode;
            form.DisplayIndex = model.DisplayIndex;
            form.ParentFormId = model.ParentFormId;
            _formRepository.Update(form);
            _formRepository.commit();
            return null;
        }
    }
}
