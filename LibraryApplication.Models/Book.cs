using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Models
{
    /// <summary>
    /// The book model.
    /// </summary>
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int NumberOfCopies { get; set; }

        public string Publisher { get; set; }

        public DateTime? DateOfPublishing { get; set; }

        public DateTime? DateOfAcquiring { get; set; }

        public int NumberOfPages { get; set; }
    }
}
