using System;
using System.Collections.Generic;
using System.Text;
using LibraryApplication.DAL.Repositories.BaseRepository;
using LibraryApplication.Models;

namespace LibraryApplication.DAL.Repositories.UserRepository
{
    /// <summary>
    /// The user repository interface.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.BaseRepository.ICommandRepository{LibraryApplication.Models.User}" />
    /// <seealso cref="LibraryApplication.DAL.Repositories.BaseRepository.IQueryRepository{LibraryApplication.Models.User}" />
    public interface IUserRepository : ICommandRepository<User>, IQueryRepository<User>
    {
        List<User> GetTopUsersByActiveOverdueTime();

        List<User> GetTopUsersByOverdueTimeHistorical();

    }
}
