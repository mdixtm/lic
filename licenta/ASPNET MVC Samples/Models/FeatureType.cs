using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecomData
{
    public class FeatureType
    {
        public int Id { get; set; }
        public int FeatureNr { get; set; }
        public int ExistingValues { get; set; }
        public int MissingValues { get; set; }
    }
}
