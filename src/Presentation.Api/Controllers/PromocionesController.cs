using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Promociones.Domain.Core.Supervisor;
using Promociones.Domain.Entities;
using Promociones.Domain.Entities.Converters;
using Promociones.Domain.Entities.ViewModels;

namespace Promociones.Presentation.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PromocionesController : Controller
    {

        private readonly IPromocionSupervisor _promocionSupervisor;

        public PromocionesController(IPromocionSupervisor promocionSupervisor)
        {
            _promocionSupervisor = promocionSupervisor;          
        }

        [HttpGet]
        public async Task<IActionResult>  ListaPromociones()
        {
            try
            {              
                //return new ObjectResult();
                var promotionsList = await _promocionSupervisor.GetAllPromotionsAsync();
                return new ObjectResult(PromocionConverter.ConvertList(promotionsList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public IActionResult ListaPromocionesVigentes()
        {
            try
            {
                //return new ObjectResult();
                var promotionsList = _promocionSupervisor.GetCurrentPromotions();
                return new ObjectResult(PromocionConverter.ConvertList(promotionsList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public IActionResult ListaPromocionesPorFecha(DateTime fecha)
        {
            try
            {
                //return new ObjectResult();
                var promotionsList = _promocionSupervisor.GetPromotionsByDateAsync(fecha);
                return new ObjectResult(PromocionConverter.ConvertList(promotionsList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }



        [HttpGet]
        public  async Task<IActionResult>  VerificarEstadoDePromocion(int id)
        {
            try
            {
                //return new ObjectResult();
                var promotionStatus = await _promocionSupervisor.CheckPromotionStatus(id);
                return new ObjectResult(promotionStatus?"Vigente":"No Vigente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public IActionResult ListaPromocionesPorVenta([FromBody]VentaViewModel value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                //return new ObjectResult();
                var promotionsList = _promocionSupervisor.GetPromotionsBySale(value.MediodePago, value.TipoMediodePago,
                    value.EntidadFinanciera, value.CantidadDeCuotas, value.CategoriaProducto);
                return new ObjectResult(PromocionConverter.ConvertList(promotionsList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }



        [HttpPost]
        public async Task<IActionResult> CrearPromocion([FromBody]PromocionViewModel value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var promocion = new Promocion(value.MediosDePagosIds, value.TiposMedioPagosIds,
                value.EntidadesFinancierasIds, value.CategoriasProductosIds, value.MaxCantidadDeCuotas ?? 0,
                value.PorcentajeDecuento,value.FechaInicio,value.FechaFin,value.Activo);
            await _promocionSupervisor.InsertPromotion(promocion);
            return Created($"Promocion/{promocion.IdPromotion}", value);
        }


        [HttpPost]
        public async Task<IActionResult> EliminarPromociones([FromBody]DeleteViewModel deleteIds)
        {           
            try
            {
                //return new ObjectResult();
                await _promocionSupervisor.DeletePromotions(deleteIds.DeleteIds);
                return new ObjectResult(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}