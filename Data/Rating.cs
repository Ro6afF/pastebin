﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// A class which represents the rating structure in the database.
    /// </summary>
    public class Rating
    {
        public int Id { get; set; }
        // The Id of the paste which is commented
        public int PasteId { get; set; }
        public string Author { get; set; }
        public int Rate { get; set; }
    }
}
