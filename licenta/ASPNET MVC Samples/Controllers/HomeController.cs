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
            SetViewBags(1);
            List<FeatureViewModel> lFeatures = Features.Take(5).ToList();
            return View(lFeatures);
        }

        public ActionResult ChangePage(int id)
        {
            HomePageViewModel lHomePageVm = new HomePageViewModel();
            int lNrSkippedElements = (id - 1) * 5;
            lHomePageVm.Features = Features.Skip(lNrSkippedElements).Take(5).ToList();
            lHomePageVm.PagViewModel = new PaginationViewModel(id,Features.Count);
            SetViewBags(id);
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
            SetViewBags(1);
            return View("FeaturesStDev", lHomePageVm);
        }



        private void SetViewBags(int pageNr)
        {
            List<PointExtensionViewModel> points = Points.Skip((pageNr - 1) * 5).Take(5).ToList();
            if (points.Count > 0)
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(points[0].Points);
            if (points.Count > 1)
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(points[1].Points);
            if (points.Count > 2)
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(points[2].Points);
            if (points.Count > 3)
            ViewBag.DataPoints4 = JsonConvert.SerializeObject(points[3].Points);
            if (points.Count > 4)
            ViewBag.DataPoints5 = JsonConvert.SerializeObject(points[4].Points);
        }



    }
}