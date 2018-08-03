using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Promociones.Domain.Entities;
using Promociones.Domain.Entities.Entities;
using Promociones.Domain.Entities.Repositories;

namespace Promociones.Domain.Core.Supervisor
{
    public class PromocionSupervisor : IPromocionSupervisor
    {

        private readonly IPromocionRepository _promocionRepository;

        public PromocionSupervisor(IPromocionRepository promocionRepository)
        {
            _promocionRepository = promocionRepository;
        }


        public Task<List<Promocion>> GetAllPromotionsAsync(CancellationToken ct = default(CancellationToken))
        {
            var promotions = _promocionRepository.GetAllAsync(ct);
            foreach (var promotion in promotions.Result)
            {
                promotion.SetArrayValues();
            }
            return promotions;
        }

        public List<Promocion> GetCurrentPromotions(CancellationToken ct = default(CancellationToken))
        {
            var promotions = _promocionRepository.GetAllAsync(ct);
            return promotions.Result.Where(x => x.FechaFin >= DateTime.Now && x.FechaInicio <= DateTime.Now).ToList();
        }

        public List<Promocion> GetPromotionsByDateAsync(DateTime date,
            CancellationToken ct = default(CancellationToken))
        {
            var promotions = _promocionRepository.GetAllAsync(ct);
            return promotions.Result.Where(x => x.FechaFin >= date && x.FechaInicio <= date).ToList();
        }

        public List<Promocion> GetPromotionsBySale(int idPaymentMethod, int idPaymentType, int idFinancialEntity, int nrDues, int productCategory,
            CancellationToken ct = default(CancellationToken))
        {
            var result = new List<Promocion>();
            var promotions = _promocionRepository.GetAllAsync(ct).Result.Where(x => x.FechaFin >= DateTime.Now && x.FechaInicio <= DateTime.Now).ToList();
            foreach (var promotion in promotions)
            {
                if (!CheckPaymentMethod(promotion, idPaymentMethod, idPaymentType, idFinancialEntity)) continue;
                if (!CheckProductCategories(promotion, productCategory)) continue;
                if (nrDues != 0 && !(nrDues <= promotion.MaxCantidadDeCuotas)) continue;
                promotion.SetArrayValues();
                result.Add(promotion);

            }
            return result;
        }


        private static bool CheckPaymentMethod(Promocion promotion, int idPaymentMethod, int idPaymentType, int idFinancialEntity)
        {
            if (promotion.MedioPagoIds != null)
            {
                return promotion.MedioPagoIds.Any(x => x == idPaymentMethod);
            }

            if (promotion.EntidadFinancieraId != null)
            {
                return promotion.EntidadFinancieraId.Any(x => x == idFinancialEntity);
            }

            return promotion.TipoMedioPagoId == null || promotion.TipoMedioPagoId.Any(x => x == idPaymentType);
        }

        private static bool CheckProductCategories(Promocion promotion, int productCategory)
        {
            return promotion.ProductoCategoriaIds == null || promotion.ProductoCategoriaIds.Any(x => x == productCategory);
        }

        public async Task<bool> InsertPromotion(Promocion promotion)
        {
            await _promocionRepository.InsertAsync(promotion);
            return true;
        }


        public async Task<bool> UpdatePromotion(Promocion promotion, CancellationToken ct = default(CancellationToken))
        {          
            await _promocionRepository.UpdateAsync(promotion);
            return true;
        }

        public async Task<bool> DeletePromotions(int[] promotions, CancellationToken ct = default(CancellationToken))
        {
            await _promocionRepository.DeactivateAsync(promotions);
            return true;
        }

        public async Task<bool> CheckPromotionStatus(int idPromotion, CancellationToken ct = default(CancellationToken))
        {
            var promotion = await _promocionRepository.GetAsync(idPromotion);
            return promotion != null && (promotion.FechaInicio <= DateTime.Now && promotion.FechaFin >= DateTime.Now);
        }
    }
}
