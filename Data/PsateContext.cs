using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PasteContext : DbContext
    {
        public PasteContext()
            : base("name=DefaultConnection")
        {

        }

        public DbSet<Paste> Pastes { get; set; }
    }
}
