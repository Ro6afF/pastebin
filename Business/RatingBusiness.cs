using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RatingBusiness
    {
        private DBContext context;

        public RatingBusiness(DBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get the rating for a given paste
        /// </summary>
        /// <param name="pasteID">The id of the paste requested</param>
        /// <returns>The average rating</returns>
        public double Get(int? pasteID)
        {
            // Get all records for the current paste from the db
            var res = context.Ratings.Where(x => x.PasteId == pasteID);
            // Check if there are not
            if (res.Count() == 0)
            {
                return 0;
            }
            // Calculate the rating
            return (double)(res.Sum(x => x.Rate)) / res.Count();
        }

        /// <summary>
        /// Rates a paste
        /// </summary>
        /// <param name="pasteId">The id of the paste</param>
        /// <param name="value">The rating</param>
        /// <param name="author">The username of the user</param>
        /// <exception cref="InvalidOperationException">When the user is not logged in</exception>
        /// <exception cref="ArgumentException">When the paste does not exist or the value is invalid</exception>
        public void Rate(int pasteId, int value, string author)
        {
            // Check if the user is logged in
            if (author == null || author == "")
            {
                throw new InvalidOperationException("Not a registered user!");
            }
            // Check if the value is valid
            if (value <= 0 || value > 5)
            {
                throw new ArgumentException("The rating shold be in the interval [1;5]");
            }
            // Check if the paste given exists
            if (context.Pastes.Where(x => x.Id == pasteId).Count() == 0)
            {
                throw new ArgumentException("The paste does not exist");
            }
            // Remove if there is exsisting rating from the user given
            if (context.Ratings.Where(x => x.PasteId == pasteId && x.Author == author).Count() != 0)
            {
                context.Ratings.Remove(context.Ratings.Where(x => x.PasteId == pasteId && x.Author == author).First());
            }
            // Add the rate to the db
            context.Ratings.Add(new Rating { Author = author, PasteId = pasteId, Rate = value });
            context.SaveChanges();
        }
    }
}
