﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Promociones.Domain.Entities;
using Promociones.Domain.Entities.Entities;

namespace Promociones.Domain.Core.Supervisor
{
    public interface IPromocionSupervisor
    {
        Task<List<Promocion>> GetAllPromotionsAsync(CancellationToken ct = default(CancellationToken));
        List<Promocion> GetCurrentPromotions(CancellationToken ct = default(CancellationToken));
        List<Promocion> GetPromotionsByDateAsync(DateTime date, CancellationToken ct = default(CancellationToken));
        List<Promocion> GetPromotionsBySale(int idPaymentMethod, int idPaymentType, int idFinancialEntity, int nrDues,
            int productCategory, CancellationToken ct = default(CancellationToken));
        Task<bool> InsertPromotion(Promocion promotion);
        Task<bool> UpdatePromotion(Promocion promotion,
            CancellationToken ct = default(CancellationToken));

        Task<bool> DeletePromotions(int[] promotions, CancellationToken ct = default(CancellationToken));
        Task<bool> CheckPromotionStatus(int idPromotion, CancellationToken ct = default(CancellationToken));
        string CheckPromotionsCollisions(Promocion promotion, List<Promocion> activePromotions);
    }
}
