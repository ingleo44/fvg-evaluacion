using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Promociones.Domain.Entities.Entities;

namespace Promociones.Domain.Entities.Repositories
{
    public interface IPromocionRepository : IGenericRepository<Promocion>
    {
        Task<List<Promocion>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task DeactivateAsync(int[] ids);
    }
}
