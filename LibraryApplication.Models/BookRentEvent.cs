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

        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime DateToReturn { get; set; }

        public DateTime? DateOfReturn { get; set; } = null;
    }
}
