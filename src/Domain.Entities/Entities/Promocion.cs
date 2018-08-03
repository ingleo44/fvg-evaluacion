using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promociones.Domain.Entities.Entities
{
    public class Promocion
    {
        

        public Promocion(int[] idPaymentMethods, int[] idPaymentTypes, int[] idFinancialEntities,
            int[] productCategories, int nrDues, float discountPercentage, DateTime startDate, DateTime endDate,
            bool activo)
        {

            StrMedioPagoIds = idPaymentMethods == null?null:string.Join(",", idPaymentMethods); 
            StrEntidadFinancieraId = idFinancialEntities==null?null:string.Join(",", idFinancialEntities);
            StrTipoMedioPagoId = idPaymentTypes==null?null:string.Join(",", idPaymentTypes);
            StrProductoCategoriaIds = productCategories==null?null:string.Join(",", productCategories);
            MaxCantidadDeCuotas = nrDues;
            PorcentajeDescuento = discountPercentage;
            FechaInicio = startDate;
            FechaFin = endDate;
            Activo = activo;
        }

        public Promocion()
        {
        }

        public void SetId(int idPromotion) => IdPromotion = idPromotion;
        public void Deactivate() => Activo = false;
        public void SetUpdateDate(DateTime date) => FechaModificacion = date;
        public void SetCreationDate() => FechaCreacion = DateTime.Now;

        public void SetArrayValues()
        {          
            TipoMedioPagoId = string.IsNullOrEmpty(StrTipoMedioPagoId)?null:Array.ConvertAll(StrTipoMedioPagoId.Split(','),int.Parse);
            EntidadFinancieraId = string.IsNullOrEmpty(StrEntidadFinancieraId)?null: Array.ConvertAll(StrEntidadFinancieraId.Split(','), int.Parse);
            ProductoCategoriaIds = string.IsNullOrEmpty(StrProductoCategoriaIds)?null:Array.ConvertAll(StrProductoCategoriaIds.Split(','), int.Parse);
            MedioPagoIds = string.IsNullOrEmpty(StrMedioPagoIds) ?null:Array.ConvertAll(StrMedioPagoIds.Split(','), int.Parse);
        }

        [Key]
        public int IdPromotion { get; private set; }        
        [NotMapped]
        public int[] TipoMedioPagoId { get; private set; }
        [NotMapped]
        public int[] EntidadFinancieraId { get; private set; }
        [NotMapped]
        public int[] ProductoCategoriaIds { get; private set; }
        [NotMapped]
        public int[] MedioPagoIds { get; private set; }
        public string StrTipoMedioPagoId { get; private set; }
        public string StrEntidadFinancieraId { get; private set; }
        public string StrProductoCategoriaIds { get; private set; }  
        public string StrMedioPagoIds { get; private set; }
        public int? MaxCantidadDeCuotas { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public DateTime? FechaModificacion { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public float PorcentajeDescuento { get; private set; }        
        public bool Activo { get; private set; }
               
    }
}
