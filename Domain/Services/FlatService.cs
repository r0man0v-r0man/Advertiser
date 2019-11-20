using AutoMapper;
using Domain.Models.FlatModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class FlatService : IFlatService
    {
        private readonly DataManager dataManager;
        private readonly IMapper mapper;
        public FlatService(DataManager dataManager, IMapper mapper)
        {
            this.dataManager = dataManager;
            this.mapper = mapper;
        }

        public async Task<FlatModel> AddAsync(FlatModel flatModel)
        {
           var result = await dataManager.Flats
                .CreateAsync(mapper.Map<DataAccess.Entities.Flat>(flatModel))
                .ConfigureAwait(false);
           return mapper.Map<FlatModel>(result);

        }

        public async IAsyncEnumerable<FlatModel> GetAll()
        {
            var flats = dataManager.Flats.GetAllAsync().ConfigureAwait(false);
            await foreach (var flat in flats)
            {
                yield return mapper.Map<FlatModel>(flat);
            }
        }

        public Task<FlatModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
