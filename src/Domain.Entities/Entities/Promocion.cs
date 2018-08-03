using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Promociones.Domain.Entities.Entities;

namespace Promociones.Domain.Entities
{
    public class Promocion
    {
        

        public Promocion(int[] idPaymentMethods, int[] idPaymentTypes, int[] idFinancialEntities,
            int[] productCategories, int nrDues, float discountPercentage, DateTime startDate, DateTime endDate,
            bool activo)
        {
            
            StrTipoMedioPagoId = idPaymentMethods == null?null:string.Join(",", idPaymentMethods); 
            StrEntidadFinancieraId = idFinancialEntities==null?null:string.Join(",", idFinancialEntities);
            StrTipoMedioPagoId = idPaymentTypes==null?null:string.Join(",", idPaymentTypes);
            StrProductoCategoriaIds = productCategories==null?null:string.Join(",", productCategories);
            MaxCantidadDeCuotas = nrDues;
            PorcentajeDescuento = discountPercentage;
            FechaInicio = startDate;
            FechaFin = endDate;
            Activo = activo;


            //IdPaymentMethods = idPaymentMethods;
            //IdPaymentTypes = idPaymentTypes;
            //IdFinancialEntities = idFinancialEntities;
            //ProductCategories = productCategories;
            //NrDues = nrDues;
            //DiscountPercentage = discountPercentage;
        }


        public void SetId(int idPromotion)
        {
            IdPromotion = idPromotion;
        }

        public void Deactivate()
        {
            Activo = false;
        }

        public void SetArrayValues()
        {          
            TipoMedioPagoId = string.IsNullOrEmpty(StrTipoMedioPagoId)?null:Array.ConvertAll<string, int> (StrTipoMedioPagoId.Split(','),int.Parse);
            EntidadFinancieraId = string.IsNullOrEmpty(StrEntidadFinancieraId)?null: Array.ConvertAll<string, int>(StrEntidadFinancieraId.Split(','), int.Parse);
            ProductoCategoriaIds = string.IsNullOrEmpty(StrProductoCategoriaIds)?null:Array.ConvertAll<string, int>(StrProductoCategoriaIds.Split(','), int.Parse);
            MedioPagoIds = string.IsNullOrEmpty(StrMedioPagoIds) ?null:Array.ConvertAll<string, int>(StrMedioPagoIds.Split(','), int.Parse);
        }
      
        public Promocion()
        {
           
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
