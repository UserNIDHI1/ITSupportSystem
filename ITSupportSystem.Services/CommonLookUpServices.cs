using ITSupportSystem.Core1.Contracts;
using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Services
{

    public interface ICommonLookUpServices
    {
        CommonLookUp CreateCommonLookUp(CommonLookUp model);
        List<CommonLookUp> GetCommonLookUpList();
        CommonLookUpViewModel GetCommonLookUp(Guid Id);
        CommonLookUp UpdateCommonLookUp(CommonLookUp model);
        void RemoveCommonLookUp(Guid Id);
    }

    public class CommonLookUpServices : ICommonLookUpServices
    {
        IRepository<CommonLookUp> _commonlookupRepository;

        public CommonLookUpServices(IRepository<CommonLookUp> commonlookupRepository)
        {
            _commonlookupRepository = commonlookupRepository;
        }

        public CommonLookUp CreateCommonLookUp(CommonLookUp model)
        {
            CommonLookUp commonLookUp = _commonlookupRepository.Collection().Where(a => a.ConfigName == model.ConfigName && a.ConfigKey == model.ConfigKey && !a.IsDeleted).FirstOrDefault();
            if (commonLookUp == null)
            {
                CommonLookUp commonlook = new CommonLookUp();
                commonlook.ConfigName = model.ConfigName;
                commonlook.ConfigKey = model.ConfigKey;
                commonlook.ConfigValue = model.ConfigValue;
                commonlook.DisplayOrder = model.DisplayOrder;
                commonlook.Description = model.Description;
                _commonlookupRepository.Insert(commonlook);
                _commonlookupRepository.Commit();
                return commonlook;
            }
            else
            {
                return null;
            }
        }

        public CommonLookUpViewModel GetCommonLookUp(Guid Id)
        {
            CommonLookUp commonlook = _commonlookupRepository.Find(Id);
            CommonLookUpViewModel commonviewmodel = new CommonLookUpViewModel();
            commonviewmodel.ConfigName = commonlook.ConfigName;
            commonviewmodel.ConfigKey = commonlook.ConfigKey;
            commonviewmodel.ConfigValue = commonlook.ConfigValue;
            commonviewmodel.DisplayOrder = commonlook.DisplayOrder;
            commonviewmodel.Description = commonlook.Description;
            return commonviewmodel;
        }

        public List<CommonLookUp> GetCommonLookUpList()
        {
            return _commonlookupRepository.Collection().Where(x => !x.IsDeleted).ToList();
        }

        public void RemoveCommonLookUp(Guid Id)
        {
            CommonLookUp commonlook = _commonlookupRepository.Collection().Where(x => x.Id == Id).FirstOrDefault();
            commonlook.IsDeleted = true;
            _commonlookupRepository.Update(commonlook);
            _commonlookupRepository.Commit();
        }

        public CommonLookUp UpdateCommonLookUp(CommonLookUp model)
        {
            CommonLookUp commonLookUp = _commonlookupRepository.Collection().Where(a => a.ConfigName == model.ConfigName && a.ConfigKey == model.ConfigKey && a.Id != model.Id && !a.IsDeleted).FirstOrDefault();
            if (commonLookUp == null)
            {
                CommonLookUp commonlook = _commonlookupRepository.Collection().Where(a => a.Id == model.Id).FirstOrDefault();

                commonlook.ConfigName = model.ConfigName;
                commonlook.ConfigKey = model.ConfigKey;
                commonlook.ConfigValue = model.ConfigValue;
                commonlook.DisplayOrder = model.DisplayOrder;
                commonlook.Description = model.Description;
                commonlook.UpdatedOn = DateTime.Now;
                _commonlookupRepository.Update(commonlook);
                _commonlookupRepository.Commit();
                return commonlook;
            }
            else
            {
                return null;
            }
        }
    }
}
