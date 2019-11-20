using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AdvertiserContext advertiserContext;
        private readonly DbSet<T> dbSet;
        public Repository(AdvertiserContext advertiserContext)
        {
            this.advertiserContext = advertiserContext;
            this.dbSet = advertiserContext.Set<T>();
        }
        public async Task<T> CreateAsync(T item)
        {
            await dbSet.AddAsync(item);
            await advertiserContext.SaveChangesAsync().ConfigureAwait(false);
            return item;
        }

        public async IAsyncEnumerable<T> GetAllAsync()
        {
            await foreach (var T in dbSet.AsAsyncEnumerable().ConfigureAwait(false))
            {
                yield return T;
            }

        }

        public async Task<T> GetByIdAsync(int Id) => await dbSet.FindAsync(Id);

        public async Task RemoveAsync(T item)
        {
            dbSet.Remove(item);
            await advertiserContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            advertiserContext.Entry(item).State = EntityState.Modified;
            return (await advertiserContext.SaveChangesAsync().ConfigureAwait(false)) > 0;
        }
    }
}
