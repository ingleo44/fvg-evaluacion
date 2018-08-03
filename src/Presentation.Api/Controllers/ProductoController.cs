using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Promociones.Presentation.Api.Controllers
{
    public class ProductoController : Controller
    {

        private readonly List<Categoria> _listaCategorias = new List<Categoria>();
        public ProductoController()
        {

            _listaCategorias.Add(new Categoria{Id=1,Descripcion = "Electrodomesticos"});
            _listaCategorias.Add(new Categoria { Id = 2, Descripcion = "Ropa Dama" });
            _listaCategorias.Add(new Categoria { Id = 3, Descripcion = "Ropa Caballeros" });
            _listaCategorias.Add(new Categoria { Id = 4, Descripcion = "Calzado Dama" });
            _listaCategorias.Add(new Categoria { Id = 5, Descripcion = "Calzado Caballero" });
            _listaCategorias.Add(new Categoria { Id = 6, Descripcion = "Muebles" });
        }

        // POST: Producto/Create
        [HttpGet ("api/[controller]/Categoria")]
        public ActionResult Categorias(IFormCollection collection)
        {
            try
            {
                return new ObjectResult(_listaCategorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

       
    }


    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

}