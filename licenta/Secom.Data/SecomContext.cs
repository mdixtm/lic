using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secom.Data
{
    public class SecomContext: DbContext
    {

        public SecomContext(): base()
        {
            Database.SetInitializer(new SecomDbInitializer());
        }

        public DbSet<Feature> Features { get; set; }
    }
}
