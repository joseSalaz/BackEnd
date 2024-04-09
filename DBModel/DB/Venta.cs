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
    [JsonIgnore]
    public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();
    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
<<<<<<< HEAD
    [JsonIgnore]
=======

<<<<<<< HEAD
=======
<<<<<<< HEAD

=======
>>>>>>> 2b79de355a32ab4478948a79c4c1c2456419a8d5
>>>>>>> 2d0c8cd3375399bc16abc62f9eb0732d56e11dd5
>>>>>>> d14815a8ea96e4f31153b7deaa5a5a4f016dafda
    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
