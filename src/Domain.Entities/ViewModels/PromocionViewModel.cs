using System;
using System.Collections.Generic;
using System.Text;
using Promociones.Domain.Entities.Entities;

namespace Promociones.Domain.Entities.ViewModels
{
    public class PromocionViewModel
    {
        public int IdPromotion { get;  set; }        
        public int[] TiposMedioPagosIds { get;  set; }
        public int[] EntidadesFinancierasIds { get;  set; }
        public int[] CategoriasProductosIds { get;  set; }
        public int[] MediosDePagosIds { get;  set; }
        public int? MaxCantidadDeCuotas { get;  set; }
        public DateTime FechaInicio { get;  set; }
        public DateTime FechaFin { get;  set; }
        public float PorcentajeDecuento { get;  set; }        
        public bool Activo { get;  set; }
    }
}
