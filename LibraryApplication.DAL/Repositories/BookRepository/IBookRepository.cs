using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryApplication.DAL.Repositories.BaseRepository;
using LibraryApplication.Models;

namespace LibraryApplication.DAL.Repositories.BookRepository
{
    /// <summary>
    /// The book repository interface.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.BaseRepository.IQueryRepository{LibraryApplication.Models.Book}" />
    public interface IBookRepository : IQueryRepository<Book>
    {
        /// <summary>
        /// Checks the number of copies available.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// Returns the number of copies available for book matching the identifier.
        /// </returns>
        int CheckNumberOfCopiesAvailable(int bookId);
    }
}
