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
    /// A class which is a context to interaact with the DB
    /// </summary>
    public class PasteContext : DbContext
    {
        public PasteContext()
            : base("name=DefaultConnection")
        {

        }

        // A set of all entries
        public virtual DbSet<Paste> Pastes { get; set; }

        // Enables mocking of the function Entry
        public virtual DbEntityEntry<Paste> Entry(Paste p)
        {
            return base.Entry(p);
        }
    }
}
