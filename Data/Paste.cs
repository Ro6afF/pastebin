﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// A class which represents the paste structure in the database.
    /// </summary>
    public class Paste
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string Description { get; set; }
        [DisplayName("Is Hidden")]
        public bool IsHidden { get; set; }
        public string AuthorID { get; set; }
        public DateTime Expieres { get; set; }
    }
}