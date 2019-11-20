using Domain.Models.FlatModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFlatService
    {
        Task<FlatModel> GetByIdAsync(int id);
        IAsyncEnumerable<FlatModel> GetAll();
        Task<FlatModel> AddAsync(FlatModel flat);
    }

}
