using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Promociones.Domain.Entities;
using Promociones.Domain.Entities.Repositories;

namespace Promociones.Infrastructure.Repositories
{
    public class PromocionRepository : GenericRepository<Promocion>, IPromocionRepository
    {
        public PromocionRepository(PromocionesDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Promocion>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await DbContext.Promociones.Where(x => x.Activo).ToListAsync(cancellationToken: ct);
        }

        public async Task DeactivateAsync(int[] ids)
        {
            foreach (var id in ids)
            {
                var entity = DbContext.Promociones.FindAsync(id).Result;
                entity.Deactivate();
                DbContext.Entry(entity).State = EntityState.Modified;

            }            
            await DbContext.SaveChangesAsync();
        }

    }
}
