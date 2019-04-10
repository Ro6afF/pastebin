using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// A class which is a context to interaact with the DB of ratings
    /// </summary>
    public class RatingContext : DbContext
    {
        public RatingContext()
            : base("name=RatingContext")
        {

        }

        // A set of all entries
        public virtual DbSet<Rating> Ratings { get; set; }

        // Enables mocking of the function Entry
        public virtual DbEntityEntry<Rating> Entry(Rating p)
        {
            return base.Entry(p);
        }
    }
}
