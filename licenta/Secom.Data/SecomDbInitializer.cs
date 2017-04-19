using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secom.Data
{
    public class SecomDbInitializer : CreateDatabaseIfNotExists<SecomContext>
    {
        protected override void Seed(SecomContext context)
        {
            IList<Feature> defaultStandards = new List<Feature>();

            defaultStandards.Add(new Feature() { FeatureNr = 1});
            defaultStandards.Add(new Feature() { FeatureNr = 2});

            foreach (Feature std in defaultStandards)
                context.Features.Add(std);

            base.Seed(context);
        }
    }
}
