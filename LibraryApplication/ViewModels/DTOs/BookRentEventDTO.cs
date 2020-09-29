using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.ViewModels.DTOs
{
    /// <summary>
    /// The book rent event data transfer object.
    /// </summary>
    public class BookRentEventDTO
    {
        public int? NumberOfCopiesRented { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int? NumberOfCopiesReturned { get; set; }

        public DateTime? DateOfRenting { get; set; }
    }
}