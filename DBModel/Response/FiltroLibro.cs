using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
  public class FiltroLibro
  {
    public class FiltroLibroRequest
    {
      public int? IdCategoria { get; set; }

      public int? IdSubcategoria { get; set; }

      public List<int>? Autores { get; set; }

      public List<int>? Proveedores { get; set; }

      public decimal? PrecioMinimo { get; set; }

      public decimal? PrecioMaximo { get; set; }
    }

    public class LibroFiltroResponse
    {
      public int IdLibro { get; set; }
      public string? Titulo { get; set; }
      public string? Imagen { get; set; }
      public decimal? PrecioVenta { get; set; }
      public string? RazonSocial { get; set; }
    }
  }
}
