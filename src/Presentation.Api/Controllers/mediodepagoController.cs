using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Promociones.Presentation.Api.Controllers
{
 
    public class MediodepagoController : Controller
    {
        /*
         *entidades financieras
         * null: ninguna
         * 1: Banco Galicia
         * 2: BBVA Frances
         *
         * Tipos de pago
         * 1: Efectivo
         * 2: Visa
         * 3: Amex
         * 4: MasterCard
         */

        private readonly List<MedioPago> _mediosPagos = new List<MedioPago>();

        public MediodepagoController()
        {
            _mediosPagos.Add(new MedioPago{Id=1,Descripcion = "Tarjeta Visa Galicia Gold", Activo = true , IdEntidadFinanciera = 1, IdTipoPago = 2 });
            _mediosPagos.Add(new MedioPago{Id = 2, Descripcion = "Tarjeta Amex Frances Platinium", Activo = true, IdEntidadFinanciera = 2, IdTipoPago = 3 });
            _mediosPagos.Add(new MedioPago{Id = 3, Descripcion = "Efectivo Pesos", Activo = true, IdEntidadFinanciera = null, IdTipoPago = 1 });
            _mediosPagos.Add(new MedioPago{ Id = 4, Descripcion = "Efectivo Dollar", Activo = true, IdEntidadFinanciera = null,IdTipoPago = 1});
        }



        // GET: mediodepago
        [HttpGet("api/[controller]")]
        public ActionResult Index()
        {
            try
            {
                return new ObjectResult(_mediosPagos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("api/[controller]/{id}")]
        // GET: mediodepago
        public ActionResult Index(int id)
        {
            try
            {
                var listaMediosPagos = _mediosPagos.Where(x => x.Id == id && x.Activo);
                return !listaMediosPagos.Any() ? (ActionResult) NotFound() : new ObjectResult(listaMediosPagos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


    }



    public class MedioPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int? IdEntidadFinanciera { get; set; } 
        public int? IdTipoPago { get; set; }
        public bool Activo { get; set; }

    }
}