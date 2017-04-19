using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secom.Data
{
    public class Feature
    {
        public int Id { get; set; }
        public int FeatureNr { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Mean { get; set; }
        public double StDev { get; set; }
        public double Median { get; set; }
    }
}
