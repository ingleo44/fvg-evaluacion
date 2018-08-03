using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Promociones.Domain.Core.Supervisor;
using Promociones.Domain.Entities;
using Promociones.Domain.Entities.Converters;
using Promociones.Domain.Entities.ViewModels;
using System.Linq;
using Newtonsoft.Json;
using Promociones.Domain.Entities.Entities;

namespace Promociones.Presentation.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PromocionesController : Controller
    {

        private readonly IPromocionSupervisor _promocionSupervisor;
        private static readonly HttpClient Client = new HttpClient();

        public PromocionesController(IPromocionSupervisor promocionSupervisor)
        {
            _promocionSupervisor = promocionSupervisor;
        }

        [HttpGet]
        public async Task<IActionResult> ListaPromociones()
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
        public async Task<IActionResult> VerificarEstadoDePromocion(int id)
        {
            try
            {
                //return new ObjectResult();
                var promotionStatus = await _promocionSupervisor.CheckPromotionStatus(id);
                return new ObjectResult(promotionStatus ? "Vigente" : "No Vigente");
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
        public async Task<IActionResult> ActualizarPromocion([FromBody]PromocionViewModel value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var promocion = new Promocion(value.MediosDePagosIds, value.TiposMedioPagosIds,
                value.EntidadesFinancierasIds, value.CategoriasProductosIds, value.MaxCantidadDeCuotas ?? 0,
                value.PorcentajeDecuento, value.FechaInicio, value.FechaFin, value.Activo);
            promocion.SetId(value.IdPromotion);
            promocion.SetArrayValues();
            var baseUri = "http://localhost:61556/";
            var path = $"{baseUri}api/mediodepago/";
            // Obtenemos todos los medios de pago para realizar las validaciones
            var mediosDePagos = await GetMedioDePagoAsync(path);
            path = $"{baseUri}api/Producto/CategoriaProducto";
            var categoriasProducto = await GetProductCategories(path);
            // validamos que los medios de pago sean validos
            if (promocion.MedioPagoIds != null)
            {
                var mediosDePagosIds = mediosDePagos.Select(x => x.Id).ToArray();
                if (promocion.MedioPagoIds.Except(mediosDePagosIds).Any())
                    return StatusCode(500, "Medio de pago no valido");
            }
            // Validamos que las entidades financieras sean validas
            if (promocion.EntidadFinancieraId != null)
            {
                var entidadesFinancierasIds = mediosDePagos.Select(x => x.IdEntidadFinanciera ?? 0).Distinct().ToArray();
                if (promocion.EntidadFinancieraId.Except(entidadesFinancierasIds).Any())
                    return StatusCode(500, "Entidad Financiera no valida");
            }
            // Validamos que los tipos de medios de pago sean validos
            if (promocion.TipoMedioPagoId != null)
            {
                var tiposDePagosIds = mediosDePagos.Select(x => x.IdTipoPago ?? 0).Distinct().ToArray();
                if (promocion.TipoMedioPagoId.Except(tiposDePagosIds).Any())
                    return StatusCode(500, "Tipo Medio de pago no valido");
            }
            // validamos que las categorias de producto sean validas
            if (promocion.ProductoCategoriaIds != null)
            {
                var categoriasDeProductos = categoriasProducto.Select(x => x.Id).Distinct().ToArray();
                if (promocion.ProductoCategoriaIds.Except(categoriasDeProductos).Any())
                    return StatusCode(500, "Categoria de producto no valida");
            }
            

            await _promocionSupervisor.UpdatePromotion(promocion);
            return Ok();
        }


        private static async Task<List<MedioPago>> GetMedioDePagoAsync(string path)
        {
            var response = await Client.GetAsync(path);
            if (!response.IsSuccessStatusCode) return null;
            var stringResult = await response.Content.ReadAsStringAsync();
            var mediosPagos = JsonConvert.DeserializeObject<List<MedioPago>>(stringResult);
            return mediosPagos;

        }

        private static async Task<List<ProductCategory>> GetProductCategories(string path)
        {
            var response = await Client.GetAsync(path);
            if (!response.IsSuccessStatusCode) return null;
            var stringResult = await response.Content.ReadAsStringAsync();
            var categoriasProducto = JsonConvert.DeserializeObject<List<ProductCategory>>(stringResult);
            return categoriasProducto;

        }



        [HttpPost]
        public async Task<IActionResult> CrearPromocion([FromBody]PromocionViewModel value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var promocion = new Promocion(value.MediosDePagosIds, value.TiposMedioPagosIds,
                value.EntidadesFinancierasIds, value.CategoriasProductosIds, value.MaxCantidadDeCuotas ?? 0,
                value.PorcentajeDecuento, value.FechaInicio, value.FechaFin, value.Activo);
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