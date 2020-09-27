using System;
using System.Collections.Generic;
using System.Text;
using LibraryApplication.DAL.Repositories.BaseRepository;
using LibraryApplication.Models;

namespace LibraryApplication.DAL.Repositories.BookRentEventRepository
{
    /// <summary>
    /// The book rent event repository interface.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.BaseRepository.ICommandRepository{LibraryApplication.Models.BookRentEvent}" />
    /// <seealso cref="LibraryApplication.DAL.Repositories.BaseRepository.IQueryRepository{LibraryApplication.Models.BookRentEvent}" />
    public interface IBookRentEventRepository : ICommandRepository<BookRentEvent>
    {
        /// <summary>
        /// Gets the book rent history by book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// Returns all rents of a specific book.
        /// </returns>
        public List<BookRentEvent> GetBookRentHistoryByBook(int bookId);

        /// <summary>
        /// Gets the book rent history by user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns all book rents by a specific user.
        /// </returns>
        public List<BookRentEvent> GetBookRentHistoryByUser(int userId);
    }
}
