using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class DocSalida
{
    public int IdDocSalida { get; set; }

    public int? NroDocSalida { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdSucursal { get; set; }

    public int IdTipoDocSalida { get; set; }

    public virtual ICollection<DetalleDocSalida> DetalleDocSalida { get; set; } = new List<DetalleDocSalida>();

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual TipoDocSalida IdTipoDocSalidaNavigation { get; set; } = null!;
}
