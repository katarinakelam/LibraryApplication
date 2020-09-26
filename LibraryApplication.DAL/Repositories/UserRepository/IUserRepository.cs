using System;
using System.Collections.Generic;
using System.Text;
using LibraryApplication.DAL.Repositories.BaseRepository;
using LibraryApplication.Models;

namespace LibraryApplication.DAL.Repositories.UserRepository
{
    public interface IUserRepository :ICRUDRepository<User>
    {
    }
}
