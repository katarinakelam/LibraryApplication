using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Models
{
    /// <summary>
    /// The book rent event model.
    /// </summary>
    public class BookRentEvent
    {
        public int Id { get; set; }

        public DateTime DateOfRenting { get; set; }

        public int NumberOfCopiesRented { get; set; } = 1;

        public int NumberOfCopiesReturned { get; set; } = 1;

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime DateToReturn { get; set; }

        public DateTime? DateOfReturn { get; set; } = null;

        public virtual Book Book { get; set; }

        public virtual User User { get; set; }
    }
}
