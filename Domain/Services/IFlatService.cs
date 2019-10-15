using Domain.Models.FlatModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFlatService
    {
        Task<FlatModel> GetByIdAsync(int id);
        IAsyncEnumerable<FlatModel> GetAll();
        Task AddAsync(FlatModel flat);
    }

}
