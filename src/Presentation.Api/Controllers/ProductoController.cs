using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Promociones.Domain.Entities.Entities;

namespace Promociones.Presentation.Api.Controllers
{
    public class ProductoController : Controller
    {

        private readonly List<CategoriaProducto> _listaCategorias = new List<CategoriaProducto>();
        public ProductoController()
        {

            _listaCategorias.Add(new CategoriaProducto{Id=1,Descripcion = "Electrodomesticos"});
            _listaCategorias.Add(new CategoriaProducto { Id = 2, Descripcion = "Ropa Dama" });
            _listaCategorias.Add(new CategoriaProducto { Id = 3, Descripcion = "Ropa Caballeros" });
            _listaCategorias.Add(new CategoriaProducto { Id = 4, Descripcion = "Calzado Dama" });
            _listaCategorias.Add(new CategoriaProducto { Id = 5, Descripcion = "Calzado Caballero" });
            _listaCategorias.Add(new CategoriaProducto { Id = 6, Descripcion = "Muebles" });
        }

        // POST: Producto/Create
        [HttpGet ("api/[controller]/CategoriaProducto")]
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
}