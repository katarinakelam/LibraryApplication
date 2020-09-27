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
    public interface IBookRentEventRepository : ICommandRepository<BookRentEvent>, IQueryRepository<BookRentEvent>
    {
    }
}
