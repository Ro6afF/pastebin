using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Business
{
    /// <summary>
    /// This class provides context for interacting with the database.
    /// </summary>
    public class PasteBusiness
    {
        private PasteContext pasteContext;

        public PasteBusiness(PasteContext context)
        {
            pasteContext = context;
        }

        /// <summary>
        /// This method gets all the records from the DB, which are neither hidden nor expiered.
        /// </summary>
        /// <returns>All the records from the DB, which are neither hidden nor expiered</returns>
        public List<Paste> GetAll()
        {
            // Get all records from the DB
            return pasteContext.Pastes.Where(x => !x.IsHidden && DateTime.Now < x.Expieres).ToList();
        }

        /// <summary>
        /// This methor returns all the pastes, which the given author has created
        /// </summary>
        /// <param name="authorID">The userID of the author</param>
        /// <returns>All the pastes created by the given author</returns>
        public List<Paste> GetAllByAuthor(string authorID)
        {
            // Get all records which are creaded by the given user
            if (authorID == null)
            {
                return new List<Paste>();
            }
            return pasteContext.Pastes.Where(x => x.AuthorID == authorID).ToList();
        }
        /// <summary>
        /// This method finds a paste by the given id and returns it
        /// </summary>
        /// <param name="id">The id of the paste</param>
        /// <returns>The post with the given id</returns>
        /// <exception cref="ArgumentException">Thrown when the given ID is invalid</exception>
        /// <exception cref="KeyNotFoundException">Thrown when a record with the given ID does not exist</exception>
        public Paste Get(int? id)
        {
            // Check if the ID is valid
            if (id == null)
            {
                throw new ArgumentException("Id cannot be null");
            }
            try
            {
                // Find the record by ID
                var result = pasteContext.Pastes.Where(x => x.Id == id).First();
                return result;
            }
            catch (InvalidOperationException)
            {
                // There is no such a record
                throw new KeyNotFoundException("No such a record in the DB");
            }
        }
        /// <summary>
        /// Add the given paste into the DB
        /// </summary>
        /// <param name="paste">The paste to be added to the DB</param>
        public void Add(Paste paste)
        {
            // Add new record in the DB
            pasteContext.Pastes.Add(paste);
            pasteContext.SaveChanges();
        }
        /// <summary>
        /// Changes the values of the record from the DB with the same ID as the give. It sets the properties of the given to the one in the database.
        /// </summary>
        /// <param name="paste">The updated paste</param>
        /// <param name="authorID">The userID of the current user</param>
        /// <exception cref="InvalidOperationException">Thrown when the current user is not the author</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the given paste does not exist in the database.</exception>
        public void Update(Paste paste, string authorID)
        {
            bool didIthrow = false;
            try
            {
                // Find the record by id
                var item = pasteContext.Pastes.Where(x => x.Id == paste.Id).First();
                // Check if the user can edit the record
                if (item.AuthorID != authorID || authorID == null)
                {
                    didIthrow = true;
                    throw new InvalidOperationException("The user is not permited to do this!");
                }
                // Update the record
                pasteContext.Entry(item).CurrentValues.SetValues(paste);
                pasteContext.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                if (didIthrow)
                {
                    throw e;
                }
                // There is no such a record
                throw new KeyNotFoundException("Record doesn't exist");
            }
        }

        /// <summary>
        /// Deletes the paste with the given ID from the DB.
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
                var paste = pasteContext.Pastes.Where(x => x.Id == id).First();
                // Check if the user is permited to do so
                if (paste.AuthorID != authorID || authorID == null)
                {
                    didIrise = true;
                    throw new InvalidOperationException("The user is not permited to do this!");
                }
                // Delete the record
                pasteContext.Pastes.Remove(paste);
                pasteContext.SaveChanges();
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
