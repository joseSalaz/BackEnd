using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class DocEntrada
{
    public int IdDocEntrada { get; set; }

    public DateTime? Fecha { get; set; }

    public string? NroDocEntrada { get; set; }

    public int IdTipoDocEntrada { get; set; }

    public int IdSucursal { get; set; }

    public int IdProveedor { get; set; }

    public virtual ICollection<DetalleDocEntrada> DetalleDocEntrada { get; set; } = new List<DetalleDocEntrada>();

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual TipoDocEntrada IdTipoDocEntradaNavigation { get; set; } = null!;
}
