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

    public int IdPersona { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

<<<<<<< HEAD

=======
>>>>>>> 2b79de355a32ab4478948a79c4c1c2456419a8d5
    public virtual Persona IdPersonaNavigation { get; set; } = null!;


    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
