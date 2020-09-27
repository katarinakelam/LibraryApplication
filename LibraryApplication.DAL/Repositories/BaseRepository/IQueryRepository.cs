using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DAL.Repositories.BaseRepository
{
    /// <summary>
    /// The query repository interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryRepository<T>
    {
        /// <summary>
        /// Gets the item specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns the item that matches with the identifier.</returns>
        T GetById(int id);

        /// <summary>
        /// Gets all items asynchronous.
        /// </summary>
        /// <returns>Returns a list of all items asynchronously.</returns>
        Task<List<T>> GetAllAsync();
    }
}
