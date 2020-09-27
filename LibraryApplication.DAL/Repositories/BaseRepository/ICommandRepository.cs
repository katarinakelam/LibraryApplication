using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DAL.Repositories.BaseRepository
{
    /// <summary>
    /// The command repository interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommandRepository<T>
    {
        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Create(T item);

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Returns the udpated item.</returns>
        T Update(T item);

        /// <summary>
        /// Deletes the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
    }
}
