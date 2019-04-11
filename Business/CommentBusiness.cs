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
        public List<Comment> GetAll(int? pasteID)
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
            context.SaveChanges();

        }

        /// <summary>
        /// Deletes the comment with the given ID from the DB.
        /// </summary>
        /// <param name="id">The id of the paste</param>
        /// <param name="authorID">The userID of the current user</param>
        /// <exception cref="InvalidOperationException">Thrown when the current user is not the author</exception>
        public void Delete(int id, string authorID)
        {
            bool didIrise = false;
            try
            {
                // Find the record by id
                var comment = context.Comments.Where(x => x.Id == id).First();
                // Check if the user is permited to do so
                if (comment.Author != authorID || authorID == null || authorID == "")
                {
                    didIrise = true;
                    throw new InvalidOperationException("The user is not permited to do this!");
                }
                // Delete the record
                context.Comments.Remove(comment);
                context.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                if (didIrise)
                {
                    throw e;
                }
            }
        }
    }
}
