using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Promociones.Domain.Entities.Entities;
using Promociones.Domain.Entities.ViewModels;

namespace Promociones.Domain.Entities.Converters
{
    public static class PromocionConverter
    {

        public static PromocionViewModel Convert(Promocion promocion)
        {
            var promocionViewModel = new PromocionViewModel();
            promocionViewModel.IdPromotion = promocion.IdPromotion;
            promocionViewModel.CategoriasProductosIds = promocion.ProductoCategoriaIds;
            promocionViewModel.EntidadesFinancierasIds = promocion.EntidadFinancieraId;
            promocionViewModel.MediosDePagosIds = promocion.MedioPagoIds;
            promocionViewModel.TiposMedioPagosIds = promocion.TipoMedioPagoId;
            promocionViewModel.FechaInicio = promocion.FechaInicio;
            promocionViewModel.FechaFin = promocion.FechaFin;
            promocionViewModel.Activo = promocion.Activo;
            promocionViewModel.MaxCantidadDeCuotas = promocion.MaxCantidadDeCuotas;
            promocionViewModel.PorcentajeDecuento = promocion.PorcentajeDescuento;

            return promocionViewModel;
        }

        public static List<PromocionViewModel> ConvertList(IEnumerable<Promocion> promociones)
        {
            return promociones.Select(a =>
                {
                    var model = new PromocionViewModel
                    {
                        IdPromotion = a.IdPromotion,
                        CategoriasProductosIds = a.ProductoCategoriaIds,
                        EntidadesFinancierasIds = a.EntidadFinancieraId,
                        MediosDePagosIds = a.MedioPagoIds,
                        TiposMedioPagosIds = a.TipoMedioPagoId,
                        FechaInicio = a.FechaInicio,
                        FechaFin = a.FechaFin,
                        Activo = a.Activo,
                        MaxCantidadDeCuotas = a.MaxCantidadDeCuotas,
                        PorcentajeDecuento = a.PorcentajeDescuento
                    };
                    return model;
                })
                .ToList();
        }

    }
}
