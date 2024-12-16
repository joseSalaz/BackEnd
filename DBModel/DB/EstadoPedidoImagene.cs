using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class EstadoPedidoImagene
{
    public int IdEstadoPedidoImagen { get; set; }

    public int IdEstadoPedido { get; set; }

    public string UrlImagen { get; set; } = null!;

    public string? Estado { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual EstadoPedido IdEstadoPedidoNavigation { get; set; } = null!;
}
