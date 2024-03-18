﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LibroResponse
    {
        public int IdLibro { get; set; }

        public string? Titulo { get; set; }

        public int? Isbn { get; set; }

        public string? Tamanno { get; set; }

        public string? Descripcion { get; set; }

        public string? Condicion { get; set; }

        public string? Impresion { get; set; }

        public string? TipoTapa { get; set; }

        public bool? Estado { get; set; }

        public int IdCategoria { get; set; }

        public int IdTipoPapel { get; set; }

        public int IdProveedor { get; set; }

        public IFormFile Imagen { get; set; }

        public string RutaImagen { get; set; }
    }
}
