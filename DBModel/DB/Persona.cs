using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBModel.DB;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Correo { get; set; }

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? Telefono { get; set; }

    public string? Sub { get; set; }
    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    [JsonIgnore]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
