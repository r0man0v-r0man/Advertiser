using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task CreateAsync(T item);
        Task<T> GetByIdAsync(int Id);
        IAsyncEnumerable<T> GetAllAsync();
        Task RemoveAsync(T item);
        Task<bool> UpdateAsync(T item);

    }
}
