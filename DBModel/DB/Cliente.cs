using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int? CodigoCliente { get; set; }

    public int IdPersona { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;
}
