using ASPNET_MVC_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Samples.ViewModels
{
    public class HomePageViewModel
    {
        public List<FeatureViewModel> Features { get; set; }
        public PaginationViewModel PagViewModel {get;set;}
    }
}