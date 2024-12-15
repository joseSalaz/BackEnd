using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class DetalleVenta
{
    public int IdLibro { get; set; }

    public string? NombreProducto { get; set; }

    public decimal? PrecioUnit { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Importe { get; set; }

    public int IdDetalleVentas { get; set; }

    public int? IdVentas { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<EstadoPedido> EstadoPedidos { get; set; } = new List<EstadoPedido>();

    public virtual Libro IdLibroNavigation { get; set; } = null!;

    public virtual Venta? IdVentasNavigation { get; set; }
}
