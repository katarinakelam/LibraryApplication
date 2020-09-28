using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.ViewModels
{
    public class BookRentEventViewModel
    {
        public DateTime DateOfRenting { get; set; }

        public int NumberOfCopiesRented { get; set; }

        public int NumberOfCopiesReturned { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public DateTime DateToReturn { get; set; }

        public DateTime? DateOfReturn { get; set; }
    }
}
