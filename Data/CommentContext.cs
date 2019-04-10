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
    public class CommentContext : DbContext
    {
        public CommentContext()
            : base("name=CommentContext")
        {

        }

        // A set of all entries
        public virtual DbSet<Comment> Comments { get; set; }

        // Enables mocking of the function Entry
        public virtual DbEntityEntry<Comment> Entry(Comment p)
        {
            return base.Entry(p);
        }
    }
}
