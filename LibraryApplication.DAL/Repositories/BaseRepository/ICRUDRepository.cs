using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApplication.DAL.Repositories.BaseRepository
{
    public interface ICRUDRepository<T>
    {
        void Create(T item);

        Task<T> GetAsync(int id);

        Task<List<T>> GetAllAsync();

        T Update(T item);

        bool Delete(int id);
    }
}
