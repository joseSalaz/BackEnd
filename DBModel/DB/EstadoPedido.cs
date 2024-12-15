using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class EstadoPedido
{
    public int IdEstadoPedido { get; set; }

    public int IdDetalleVentas { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaEstado { get; set; }

    public string? Comentario { get; set; }

    public virtual ICollection<EstadoPedidoImagene> EstadoPedidoImagenes { get; set; } = new List<EstadoPedidoImagene>();

    public virtual DetalleVenta IdDetalleVentasNavigation { get; set; } = null!;
}
