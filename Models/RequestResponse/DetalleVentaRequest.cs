﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DetalleVentaRequest
    {
        public int IdLibro { get; set; }

        public string? NombreProducto { get; set; }

        public decimal? PrecioUnit { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Importe { get; set; }

        public int IdDetalleVentas { get; set; }

        public int? IdVentas { get; set; }
    }
}
