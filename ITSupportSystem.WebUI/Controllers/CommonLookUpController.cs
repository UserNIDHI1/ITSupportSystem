﻿using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using ITSupportSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSupportSystem.WebUI.Controllers
{
    public class CommonLookUpController : Controller
    {

        ICommonLookUpServices _commonLookServices;
        public CommonLookUpController(ICommonLookUpServices commonLookServices)
        {
            _commonLookServices = commonLookServices;
        }

        // GET: CommonLookUp
        public ActionResult Index()
        {
            List<CommonLookUp> commonlook = _commonLookServices.GetCommonLookUpList().ToList();
            return View(commonlook);

        }

        public ActionResult Create()
        {
            CommonLookUpViewModel commonLookUp = new CommonLookUpViewModel();
            return PartialView("CreatePartialView", commonLookUp);
        }

        [HttpPost]
        public ActionResult Create(CommonLookUpViewModel model)
        {
            _commonLookServices.CreateCommonLookUp(model);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(Guid Id)
        {
            CommonLookUpViewModel commonlook = _commonLookServices.GetCommonLookUp(Id);
            if (commonlook == null)
            {
                return HttpNotFound();
            }
            return PartialView("EditPartialView", commonlook);
        }

        [HttpPost]
        public ActionResult Edit(CommonLookUpViewModel model)
        {
            _commonLookServices.UpdateCommonLookUp(model);
            return RedirectToAction("Index");
        }

        //public ActionResult Delete(Guid Id)
        //{
        //    CommonLookUpViewModel commonlook = _commonLookServices.GetCommonLookUp(Id);
        //    if (commonlook == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(commonlook);
        //}


        [HttpPost]
        public ActionResult Delete(Guid Id)
        {
            _commonLookServices.RemoveCommonLookUp(Id);
            return RedirectToAction("Index");
        }

    }
}