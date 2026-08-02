using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBModel.DB;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Cargo { get; set; }

    public bool? Estado { get; set; }

    public int IdPersona { get; set; }

    public string? NotificationToken { get; set; }
    [JsonIgnore]
    public virtual Persona IdPersonaNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
