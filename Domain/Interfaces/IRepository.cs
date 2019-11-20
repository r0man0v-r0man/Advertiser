using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T item);
        Task<T> GetByIdAsync(int Id);
        IAsyncEnumerable<T> GetAllAsync();
        Task RemoveAsync(T item);
        Task<bool> UpdateAsync(T item);

    }
}
