using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Promociones.Domain.Entities.Repositories;

namespace Promociones.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        protected PromocionesDbContext DbContext { get; set; }

        public async Task<T> GetAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public IQueryable<T> Query()
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var entity =DbContext.FindAsync<T>(id);                
                DbContext.Remove(entity);
            }
            await DbContext.SaveChangesAsync();
        }

    }
}
