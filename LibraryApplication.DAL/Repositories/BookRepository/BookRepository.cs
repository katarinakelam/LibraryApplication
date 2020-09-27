using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.DAL.Repositories.BookRepository
{
    /// <summary>
    /// The book repository.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.BookRepository.IBookRepository" />
    public class BookRepository : IBookRepository
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public BookRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the book by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns a book with matching identifier.
        /// </returns>
        public Book GetById(int id)
        {
            this.ValidateBooksPresenceInDatabase(id);

            return this.context.Books.FirstOrDefault(book => book.Id == id);
        }

        /// <summary>
        /// Gets all books asynchronous.
        /// </summary>
        /// <returns>
        /// Returns a list of all books.
        /// </returns>
        /// <exception cref="NullReferenceException">There are no books in the database.</exception>
        public Task<List<Book>> GetAllAsync()
        {
            if (!this.context.Books.Any())
                throw new NullReferenceException("There are no books in the database.");

            return this.context.Books.ToListAsync();
        }

        /// <summary>
        /// Validates the books presence in database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentOutOfRangeException">id</exception>
        /// <exception cref="NullReferenceException">
        /// There are no books in the database.
        /// or
        /// Book not found in the database.
        /// </exception>
        private void ValidateBooksPresenceInDatabase(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (!this.context.Books.Any())
                throw new NullReferenceException("There are no books in the database.");

            if (this.context.Books.FirstOrDefault(book => book.Id == id) == null)
                throw new NullReferenceException("Book not found in the database.");
        }
    }
}
