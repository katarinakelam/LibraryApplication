using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.DAL.Repositories.BookRentEventRepository
{
    /// <summary>
    /// The book rent event repository.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.BookEventRepository.IBookRentEventRepository" />
    public class BookRentEventRepository : IBookRentEventRepository
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRentEventRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public BookRentEventRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Create(BookRentEvent item)
        {
            this.ValidateBookRentEvent(item);

            this.context.BookRentEvents.Add(item);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns the updated item.
        /// </returns>
        public BookRentEvent Update(BookRentEvent item)
        {
            this.ValidateBookRentEvent(item);

            var bookRentEvent = this.context.BookRentEvents.Include(br => br.Book).Include(br => br.User)
            .FirstOrDefault(br => br.BookId == item.BookId && br.UserId == item.UserId
            && br.DateOfRenting == item.DateOfRenting && br.DateOfReturn == null); //The book was returned if DateOfReturn is not null

            bookRentEvent.NumberOfCopiesReturned += item.NumberOfCopiesReturned;

            if (item.NumberOfCopiesReturned == bookRentEvent.NumberOfCopiesRented)
                bookRentEvent.DateOfReturn = DateTime.Today;

            this.context.Entry(bookRentEvent).State = EntityState.Modified;
            this.context.SaveChanges();

            return bookRentEvent;
        }

        /// <summary>
        /// Gets the book rent history by book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// Returns all rents of a specific book.
        /// </returns>
        /// <exception cref="NullReferenceException">There are no book rent events in the database.</exception>
        public List<BookRentEvent> GetBookRentHistoryByBook(int bookId)
        {
            return this.GetBookRentHistory(BookRentHistoryType.BookRentHistoryByBook, bookId);
        }

        /// <summary>
        /// Gets the book rent history by user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Returns all book rents by a specific user.
        /// </returns>
        /// <exception cref="NullReferenceException">There are no book rent events in the database.</exception>
        public List<BookRentEvent> GetBookRentHistoryByUser(int userId)
        {
            return this.GetBookRentHistory(BookRentHistoryType.BookRentHistoryByUser, userId);
        }

        /// <summary>
        /// Validates the book rent event.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        /// <exception cref="ValidationException">
        /// Renting user" + basicValidationExceptionMessage
        /// or
        /// Renting book" + basicValidationExceptionMessage
        /// or
        /// Renting dates" + basicValidationExceptionMessage
        /// </exception>
        private void ValidateBookRentEvent(BookRentEvent item)
        {
            var basicValidationExceptionMessage = " invalid. Please re-input the data and try again.";

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.UserId <= 0)
                throw new ValidationException("Renting user" + basicValidationExceptionMessage);

            if (item.BookId <= 0)
                throw new ValidationException("Renting book" + basicValidationExceptionMessage);

            if (item.DateOfRenting == DateTime.MinValue || item.DateToReturn == DateTime.MinValue)
                throw new ValidationException("Renting dates" + basicValidationExceptionMessage);
        }

        /// <summary>
        /// Validates the book rent events presence in database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentOutOfRangeException">id</exception>
        /// <exception cref="NullReferenceException">
        /// There are no book rent events in the database.
        /// or
        /// Book rent event not found in the database.
        /// </exception>
        private void ValidateBookRentEventsPresenceInDatabase(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (!this.context.BookRentEvents.Any())
                throw new NullReferenceException("There are no book rent events in the database.");

            if (this.context.BookRentEvents.AsNoTracking().FirstOrDefault(br => br.Id == id) == null)
                throw new NullReferenceException("Book rent event not found in the database.");
        }

        /// <summary>
        /// Gets the book rent history.
        /// </summary>
        /// <param name="bookRentHistoryType">Type of the book rent history.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns the book rent history by book or user.
        /// </returns>
        /// <exception cref="NullReferenceException">There are no book rent events in the database.</exception>
        private List<BookRentEvent> GetBookRentHistory(BookRentHistoryType bookRentHistoryType, int id)
        {
            if (!this.context.BookRentEvents.Any())
                throw new NullReferenceException("There are no book rent events in the database.");

            try
            {
                return this.context.BookRentEvents.AsNoTracking().Include(br => br.User).Include(br => br.Book)
                    .Where(b => bookRentHistoryType == BookRentHistoryType.BookRentHistoryByBook
                    ? b.BookId == id : b.UserId == id)
                    .OrderByDescending(br => br.DateOfRenting)
                    .ToList();
            }
            catch (Exception ex)
            {
                return new List<BookRentEvent>();
            }
        }
    }

    /// <summary>
    /// The book rent history type.
    /// </summary>
    internal enum BookRentHistoryType
    {
        BookRentHistoryByBook,

        BookRentHistoryByUser
    }
}
