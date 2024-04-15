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

    public int IdCaja { get; set; }
<<<<<<< HEAD

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Caja IdCajaNavigation { get; set; } = null!;

=======
    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    [JsonIgnore]
    public virtual Caja IdCajaNavigation { get; set; } = null!;
    [JsonIgnore]
>>>>>>> 659483f76575f0aaa47ab234b486fc7fa8bd8bea
    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
