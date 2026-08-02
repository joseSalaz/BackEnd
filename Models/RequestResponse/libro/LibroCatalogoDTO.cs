using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse.libro
{
  public class LibroCatalogoDTO
  {
    public int IdLibro { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
    public decimal PrecioVenta { get; set; }
    public string Autor { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
  }

  public class LibroCatalogo
  {
    public Libro Libro { get; set; }
    public decimal Precio { get; set; }
  }
}
