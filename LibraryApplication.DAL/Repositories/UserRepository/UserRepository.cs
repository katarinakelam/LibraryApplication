using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LibraryApplication.DAL.Repositories.UserRepository
{
    /// <summary>
    /// The user repository.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.UserRepository.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public UserRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Create(User item)
        {
            this.ValidateUser(item);

            //If such a user does not already exist in the database, create him. If yes, return an error message.
            if (!this.context.Users.Any(u => u.FirstName == item.FirstName && u.LastName == item.LastName && u.DateOfBirth == item.DateOfBirth))
            {
                this.context.Users.Add(item);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Such user already exists in the database.");
            }
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// Returns updated entity.
        /// </returns>
        public User Update(User item)
        {
            this.ValidateUser(item);

            this.ValidateUsersPresenceInDatabase(item.Id);

            this.context.Users.Update(item);
            this.context.SaveChanges();

            return item;
        }

        /// <summary>
        /// Deletes the user specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            this.ValidateUsersPresenceInDatabase(id);

            var userToRemove = this.GetById(id);
            this.context.Users.Remove(userToRemove);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Gets user specified the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns a user matching the identifier.
        /// </returns>
        public User GetById(int id)
        {
            this.ValidateUsersPresenceInDatabase(id);

            return this.context.Users.AsNoTracking().FirstOrDefault(user => user.Id == id);
        }

        /// <summary>
        /// Gets all users asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a list of all users.
        /// </returns>
        /// <exception cref="NullReferenceException">There are no users in the database.</exception>
        public Task<List<User>> GetAllAsync()
        {
            if (!this.context.Users.Any())
                throw new NullReferenceException("There are no users in the database.");

            return this.context.Users.ToListAsync();
        }

        /// <summary>
        /// Gets the top users by active overdue time.
        /// </summary>
        /// <returns>Returns a list of users having active top overdue time.</returns>
        public List<User> GetTopUsersByActiveOverdueTime()
        {
            return this.GetTopUsersByOverDueTime();
        }

        /// <summary>
        /// Finds the user by name.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>
        /// Returns a list of users matching the search string.
        /// </returns>
        /// <exception cref="ArgumentException">searchString</exception>
        /// <exception cref="NullReferenceException">There are no users in the database.</exception>
        public List<User> FindUserByName(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                throw new ArgumentException(nameof(searchString));

            if (!this.context.Users.Any())
                throw new NullReferenceException("There are no users in the database.");

            //Clear search string of unnecessary white spaces
            searchString = string.Join(' ', searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            try
            {
                return this.context.Users.AsNoTracking().Where(u => (u.FirstName + " " + u.LastName).ToLower().IndexOf(searchString.ToLower().Trim()) > -1).ToList();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }

        }

        /// <summary>
        /// Finds the user by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns>
        /// Returns a list of users matching the date of birth.
        /// </returns>
        /// <exception cref="ArgumentException">dateOfBirth</exception>
        /// <exception cref="NullReferenceException">There are no users in the database.</exception>
        public List<User> FindUserByDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth == null || dateOfBirth == DateTime.MinValue)
                throw new ArgumentException(nameof(dateOfBirth));

            if (!this.context.Users.Any())
                throw new NullReferenceException("There are no users in the database.");

            try
            {
                return this.context.Users.Where(u => u.DateOfBirth.HasValue && u.DateOfBirth.Value == dateOfBirth).ToList();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        /// <summary>
        /// Gets the top users by over due time.
        /// </summary>
        /// <param name="historical">if set to <c>true</c> [historical].</param>
        /// <returns>Returns a list of users having top overdue time.</returns>
        /// <exception cref="NullReferenceException">There are no users in the database.</exception>
        private List<User> GetTopUsersByOverDueTime()
        {
            if (!this.context.Users.Any())
                throw new NullReferenceException("There are no users in the database.");

            //If no users have ever rented the book
            if (!this.context.BookRentEvents.Any())
                return new List<User>();

            var userIds = this.context.BookRentEvents
                .Where(br => !br.DateOfReturn.HasValue) // If we're looking only at active users, they haven't returned their books yet.
                .GroupBy(br => br.UserId)
                .Select(br => new { UserId = br.Key, TotalOverDueTime = br.Sum(b => EF.Functions.DateDiffDay(b.DateOfReturn.Value, b.DateOfRenting)) })
                .OrderByDescending(br => br.TotalOverDueTime)
                .Select(br => br.UserId)
                .ToList();

            try
            {
                return this.context.Users.Where(u => userIds.Contains(u.Id)).ToList();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        /// <exception cref="ValidationException">
        /// User's name" + basicValidationExceptionMessage
        /// or
        /// User's DOB" + basicValidationExceptionMessage
        /// or
        /// User's contacts list" + basicValidationExceptionMessage
        /// </exception>
        private void ValidateUser(User item)
        {
            var basicValidationExceptionMessage = " is invalid. Please re-input the data and try again.";

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (string.IsNullOrEmpty(item.FirstName) || string.IsNullOrEmpty(item.LastName))
                throw new ValidationException("User's name" + basicValidationExceptionMessage);

            if (!item.DateOfBirth.HasValue || item.DateOfBirth == DateTime.MinValue)
                throw new ValidationException("User's DOB" + basicValidationExceptionMessage);

            if (!item.UserContacts.Any())
                throw new ValidationException("User's contacts list" + basicValidationExceptionMessage);
        }

        /// <summary>
        /// Validates the users presence in database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentOutOfRangeException">id</exception>
        /// <exception cref="NullReferenceException">
        /// There are no users in the database.
        /// or
        /// User not found in the database.
        /// </exception>
        private void ValidateUsersPresenceInDatabase(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (!this.context.Users.Any())
                throw new NullReferenceException("There are no users in the database.");

            if (this.context.Users.AsNoTracking().FirstOrDefault(user => user.Id == id) == null)
                throw new NullReferenceException("User not found in the database.");
        }
    }
}
