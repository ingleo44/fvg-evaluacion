using System;
using System.Collections.Generic;
using System.Text;

namespace Promociones.Domain.Entities.ViewModels
{
    public class VentaViewModel
    {
        public int MediodePago { get; set; }
        public int TipoMediodePago { get; set; }
        public int EntidadFinanciera { get; set; }
        public int CantidadDeCuotas { get; set; }
        public int CategoriaProducto { get; set; }

    }
}
