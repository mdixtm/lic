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
        static List<List<DataPoint>> Points = DataCreator.AllPoints();

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
            lHomePageVm.PagViewModel = new PaginationViewModel(id);
            SetViewBags(id);
            return View("Features", lHomePageVm);
        }

        private void SetViewBags(int pageNr)
        {
            List<List<DataPoint>> points = Points.Skip((pageNr - 1) * 5).Take(5).ToList();
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(points[0]);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(points[1]);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(points[2]);
            ViewBag.DataPoints4 = JsonConvert.SerializeObject(points[3]);
            ViewBag.DataPoints5 = JsonConvert.SerializeObject(points[4]);
        }



    }
}