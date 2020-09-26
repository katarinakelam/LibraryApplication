using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NumberOfCopies { get; set; }

        public string Publisher { get; set; }

        public DateTime? DateOfPublishing { get; set; }

        public DateTime? DateOfAcquiring { get; set; }

        public int NumberOfPages { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
