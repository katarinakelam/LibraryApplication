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
    public interface IUserRepository : ICommandRepository<User>, IQueryRepository<User>, IDeleteRepository
    {
        /// <summary>
        /// Gets the top users by active overdue time.
        /// </summary>
        /// <returns>Returns a list of users having active top overdue time.</returns>
        List<User> GetTopUsersByActiveOverdueTime();

        /// <summary>
        /// Gets the top users by overdue time historical.
        /// </summary>
        /// <returns>Returns a list of users having top overdue time historical.</returns>
        List<User> GetTopUsersByOverdueTimeHistorical();
    }
}
