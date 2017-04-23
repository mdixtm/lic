using ASPNET_MVC_Samples.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecomData;
using ASPNET_MVC_Samples.ViewModels;

namespace ASPNET_MVC_Samples.Controllers
{
    public class HomeController : Controller
    {
        static List<FeatureViewModel> Features = DataCreator.AllFeatures();
        static List<PointExtensionViewModel> Points = DataCreator.AllPoints();
        public ActionResult Index()
        {
            DataCreator.ReadFile();
            List<FeatureViewModel> lFeatures = Features.Take(5).ToList();
            SetViewBags(lFeatures);  
            return View(lFeatures);
        }

        public ActionResult ChangePage(int id)
        {
            HomePageViewModel lHomePageVm = new HomePageViewModel();
            int lNrSkippedElements = (id - 1) * 5;
            lHomePageVm.Features = Features.Skip(lNrSkippedElements).Take(5).ToList();
            lHomePageVm.PagViewModel = new PaginationViewModel(id,Features.Count);
            SetViewBags(lHomePageVm.Features);
            return View("Features", lHomePageVm);
        }

        public ActionResult StDevZero(int id)
        {
            DataCreator.ReadFile();
            HomePageViewModel lHomePageVm = new HomePageViewModel();
            int lNrSkippedElements = (id - 1) * 5;
            List<FeatureViewModel> lStDevZeroFeatures = new List<FeatureViewModel>();
            lStDevZeroFeatures = Features.Where(f => f.FeatureInfo.StDev == 0).ToList();
            lHomePageVm.Features = lStDevZeroFeatures.Skip(lNrSkippedElements).Take(5).ToList();
            lHomePageVm.PagViewModel = new PaginationViewModel(id, lStDevZeroFeatures.Count);
            SetViewBags(lHomePageVm.Features);
            return View("FeaturesStDev", lHomePageVm);
        }

        public ActionResult MissingValues(int id)
        {
            DataCreator.ReadFile();
            HomePageViewModel lHomePageVm = new HomePageViewModel();
            int lNrSkippedElements = (id - 1) * 5;
            List<FeatureViewModel> lMissingValues = new List<FeatureViewModel>();
            List<int> lMissing = DataCreator.MissingValuesOnColumns();
            lMissingValues = Features.Where(f => lMissing[f.FeatureNr - 1] >= (1567 *0.5)).ToList();
            lHomePageVm.Features = lMissingValues.Skip(lNrSkippedElements).Take(5).ToList();
            lHomePageVm.PagViewModel = new PaginationViewModel(id, lMissingValues.Count);
            SetViewBags(lHomePageVm.Features);
            return View(lHomePageVm);
        }


        private void SetViewBags(List<FeatureViewModel> features)
        {
            if (features.Count > 0)
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(Points[features[0].FeatureNr-1].Points);
            if (features.Count > 1)
                ViewBag.DataPoints2 = JsonConvert.SerializeObject(Points[features[1].FeatureNr-1].Points);
            if (features.Count > 2)
                ViewBag.DataPoints3 = JsonConvert.SerializeObject(Points[features[2].FeatureNr-1].Points);
            if (features.Count > 3)
                ViewBag.DataPoints4 = JsonConvert.SerializeObject(Points[features[3].FeatureNr-1].Points);
            if (features.Count > 4)
                ViewBag.DataPoints5 = JsonConvert.SerializeObject(Points[features[4].FeatureNr-1].Points);
        }



    }
}