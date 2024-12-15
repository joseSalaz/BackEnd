using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    [JsonIgnore]
    public virtual Caja IdCajaNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Persona IdPersonaNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
