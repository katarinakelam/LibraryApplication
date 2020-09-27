using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.DAL.Repositories.BaseRepository
{
    /// <summary>
    /// The delete command repository interface.
    /// </summary>
    public interface IDeleteRepository
    {
        /// <summary>
        /// Deletes the item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
    }
}
