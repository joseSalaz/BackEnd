using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Venta
{
    public int IdVentas { get; set; }

    public decimal? TotalPrecio { get; set; }

    public string? TipoComprobante { get; set; }

    public DateTime? FechaVenta { get; set; }

    public string? NroComprobante { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public int IdLibro { get; set; }

    public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();

    public virtual DetalleVenta? DetalleVenta { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Libro IdLibroNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
