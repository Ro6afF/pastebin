using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public virtual DbSet<Paste> Pastes { get; set; }
    }
}
