using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CommentBusiness
    {
        private DBContext context;

        public CommentBusiness(DBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get all comments for a given paste
        /// </summary>
        /// <param name="pasteID">The id of the paste requested</param>
        /// <returns>The list of all the comments for the given paste</returns>
        public List<Comment> GetAll(int pasteID)
        {
            return context.Comments.Where(x => x.PasteId == pasteID).ToList();
        }

        /// <summary>
        /// Add a comment to the database
        /// </summary>
        /// <param name="comment">The comment to be added</param>
        public void Add(Comment comment)
        {
            context.Comments.Add(comment);

        }
    }
}
