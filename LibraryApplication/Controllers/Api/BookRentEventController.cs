using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryApplication.DAL.Repositories.BookRentEventRepository;
using LibraryApplication.Models;
using LibraryApplication.ViewModels;
using LibraryApplication.ViewModels.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers.Api
{
    /// <summary>
    /// The library book rent event API controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/book-rents")]
    [ApiController]
    public class BookRentEventController : ControllerBase
    {
        private readonly IBookRentEventRepository bookRentEventRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRentEventController"/> class.
        /// </summary>
        /// <param name="bookRentEventRepository">The book rent event repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// bookRentEventRepository
        /// or
        /// mapper
        /// </exception>
        public BookRentEventController(IBookRentEventRepository bookRentEventRepository,
          IMapper mapper)
        {
            this.bookRentEventRepository = bookRentEventRepository ?? throw new ArgumentNullException(nameof(bookRentEventRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("rent-a-book")]
        public IActionResult CreateBookRentEvent(BookRentEventDTO bookRentEventDto)
        {
            var errorMessage = this.ValidateBookRentEventData(bookRentEventDto);
            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            try
            {
                var model = this.mapper.Map<BookRentEvent>(bookRentEventDto);
                this.bookRentEventRepository.Create(model);
                return Ok(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Updates the book rent event.
        /// </summary>
        /// <param name="bookRentEventDto">The book rent event dto.</param>
        /// <returns>
        /// Returns result of returning a book.
        /// </returns>
        [HttpPost]
        [Route("return-a-book")]
        public IActionResult UpdateBookRentEvent(BookRentEventDTO bookRentEventDto)
        {
            var errorMessage = this.ValidateBookRentEventData(bookRentEventDto);
            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            try
            {
                var model = this.mapper.Map<BookRentEvent>(bookRentEventDto);
                return Ok(this.bookRentEventRepository.Update(model));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Gets the book rent history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns book's rent history in descending order.
        /// </returns>
        [HttpGet]
        [Route("book-rent-history/{id}")]
        public IActionResult GetBookRentHistory(int id)
        {
            if (id <= 0)
                return BadRequest("Book identifier out of range.");

            try
            {
                var models = this.bookRentEventRepository.GetBookRentHistoryByBook(id);
                return Ok(this.mapper.Map<List<BookRentEventViewModel>>(models));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Gets the user rent history.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Returns user rent history in descending order.
        /// </returns>
        [HttpGet]
        [Route("user-rent-history/{id}")]
        public IActionResult GetUserRentHistory(int id)
        {
            if (id <= 0)
                return BadRequest("User identifier out of range.");

            try
            {
                var models = this.bookRentEventRepository.GetBookRentHistoryByUser(id);
                return Ok(this.mapper.Map<List<BookRentEventViewModel>>(models));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Validates the book rent event data.
        /// </summary>
        /// <param name="bookRentEventDTO">The book rent event dto.</param>
        /// <returns>
        /// Returns an error message if book rent event data is invalid.
        /// </returns>
        private string ValidateBookRentEventData(BookRentEventDTO bookRentEventDTO)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();

            if (bookRentEventDTO == null)
                errorMessageBuilder.Append("Data missing from request.");

            if (bookRentEventDTO.BookId <= 0)
                errorMessageBuilder.Append("Book data inside the request is invalid.");

            if (bookRentEventDTO.UserId <= 0)
                errorMessageBuilder.Append("User data inside the request is invalid.");

            if (bookRentEventDTO.NumberOfCopiesRented <= 0)
                errorMessageBuilder.Append("Number of copies inside the request is invalid.");

            return errorMessageBuilder.ToString();
        }
    }
}
