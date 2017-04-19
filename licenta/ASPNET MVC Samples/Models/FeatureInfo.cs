using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET_MVC_Samples.Models
{
    public class FeatureInfo
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Mean { get; set; }
        public double StDev { get; set; }
        public double Median { get; set; }

    }
}