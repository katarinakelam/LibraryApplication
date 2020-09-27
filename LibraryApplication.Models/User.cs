using System;
using System.Collections.Generic;

namespace LibraryApplication.Models
{
    /// <summary>
    /// The user model.
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<string> UserContacts { get; set; }
    }
}
