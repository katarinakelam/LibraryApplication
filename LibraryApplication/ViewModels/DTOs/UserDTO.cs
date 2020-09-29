using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.ViewModels.DTOs
{
    /// <summary>
    /// The user data transfer object.
    /// </summary>
    public class UserDTO
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<string> UserContacts { get; set; }

        public bool IsValid { get; set; }
    }
}
