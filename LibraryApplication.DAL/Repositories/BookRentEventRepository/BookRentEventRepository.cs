using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryApplication.Models;

namespace LibraryApplication.DAL.Repositories.BookRentEventRepository
{
    /// <summary>
    /// The book rent event repository.
    /// </summary>
    /// <seealso cref="LibraryApplication.DAL.Repositories.BookEventRepository.IBookRentEventRepository" />
    public class BookRentEventRepository : IBookRentEventRepository
    {
        public void Create(BookRentEvent item)
        {
            throw new NotImplementedException();
        }

        public BookRentEvent Update(BookRentEvent item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BookRentEvent GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookRentEvent>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
