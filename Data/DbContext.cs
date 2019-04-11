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
    /// A class which is a context to interaact with the DB of pastes
    /// </summary>
    public class DBContext : DbContext
    {
        public DBContext()
            : base("name=DefaultConnection")
        {

        }

        // A set of all entries
        public virtual IDbSet<Paste> Pastes { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Rating> Ratings { get; set; }

        // Enables mocking of the function Entry
        public virtual DbEntityEntry<Paste> Entry(Paste p)
        {
            return base.Entry(p);
        }
        public virtual DbEntityEntry<Comment> Entry(Comment p)
        {
            return base.Entry(p);
        }
        public virtual DbEntityEntry<Rating> Entry(Rating p)
        {
            return base.Entry(p);
        }
    }
}
