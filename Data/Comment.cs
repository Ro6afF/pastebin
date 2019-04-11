using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// A class which represents the comment structure in the database.
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }
        // The Id of the paste which is commented
        public int PasteId { get; set; }
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
