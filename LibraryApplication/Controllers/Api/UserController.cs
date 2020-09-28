using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryApplication.DAL.Repositories.UserRepository;
using LibraryApplication.Models;
using LibraryApplication.ViewModels;
using LibraryApplication.ViewModels.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LibraryApplication.Controllers.Api
{
    /// <summary>
    /// The library user API controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// userRepository
        /// or
        /// mapper
        /// </exception>
        public UserController(IUserRepository userRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Returns status code depending on request success.
        /// </returns>
        [HttpPost]
        public IActionResult Create(UserDTO user)
        {
            var errorMessage = this.ValidateUserDataRequest(user);
            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            try
            {
                var model = this.mapper.Map<User>(user);
                this.userRepository.Create(model);
                return Ok(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Returns a status code depending on request success.
        /// </returns>
        [HttpPost]
        [Route("update")]
        public IActionResult Update(UserDTO user)
        {
            var errorMessage = this.ValidateUserDataRequest(user);
            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            try
            {
                var model = this.mapper.Map<User>(user);
                this.userRepository.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the user by the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns a user with matching identifier.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest("User identifier out of range.");

            try
            {
                var model = this.userRepository.GetById(id);
                return Ok(this.mapper.Map<UserViewModel>(model));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// Returns a list of all users.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var models = await this.userRepository.GetAllAsync();
                var users = this.mapper.Map<List<UserViewModel>>(models);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the user specified by the identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns a status code depending on request success.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("User identifier out of range.");

            try
            {
                this.userRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the name of the users by.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>
        /// Returns users with matching name.
        /// </returns>
        [HttpGet]
        [Route("search-by-name")]
        public IActionResult GetUsersByName(string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
                return BadRequest("Search string is invalid.");

            try
            {
                var models = this.userRepository.FindUserByName(searchString);
                var users = this.mapper.Map<List<UserViewModel>>(models);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the users by date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns>
        /// Returns users with matching date of birth.
        /// </returns>
        [HttpGet]
        [Route("search-by-dob")]
        public IActionResult GetUsersByDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth == null || dateOfBirth == DateTime.MinValue)
                return BadRequest("Date of birth parameter is invalid.");

            try
            {
                var models = this.userRepository.FindUserByDateOfBirth(dateOfBirth);
                var users = this.mapper.Map<List<UserViewModel>>(models);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the users by overdue time.
        /// </summary>
        /// <returns>
        /// Returns a list of users having top active overdue time.
        /// </returns>
        [HttpGet]
        [Route("top-overdue-active")]
        public IActionResult GetUsersByActiveOverdueTime()
        {
            try
            {
                var models = this.userRepository.GetTopUsersByActiveOverdueTime();
                var users = this.mapper.Map<List<UserViewModel>>(models);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the users by overdue time historic.
        /// </summary>
        /// <returns>
        /// Returns a list of users having top historic overdue time.
        /// </returns>
        [HttpGet]
        [Route("top-overdue-historic")]
        public IActionResult GetUsersByOverdueTimeHistoric()
        {
            try
            {
                var models = this.userRepository.GetTopUsersByOverdueTimeHistorical();
                var users = this.mapper.Map<List<UserViewModel>>(models);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Validates the user data request.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Returns an error message if the user data model is invalid.
        /// </returns>
        private string ValidateUserDataRequest(UserDTO user)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();

            if (user == null)
                errorMessageBuilder.Append("User data missing from request.");

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                errorMessageBuilder.Append("User name is missing from the request.");

            if (!user.DateOfBirth.HasValue)
                errorMessageBuilder.Append("User DOB is missing from the request.");

            if (user.UserContacts == null || !user.UserContacts.Any())
                errorMessageBuilder.Append("User contacts are missing from the request.");

            return errorMessageBuilder.ToString();
        }
    }
}