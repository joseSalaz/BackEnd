using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string? Titulo { get; set; }

    public string? Isbn { get; set; }

    public string? Tamanno { get; set; }

    public string? Descripcion { get; set; }

    public string? Condicion { get; set; }

    public string? Impresion { get; set; }

    public string? TipoTapa { get; set; }

    public bool? Estado { get; set; }

    public int IdCategoria { get; set; }

    public int IdTipoPapel { get; set; }

    public int IdProveedor { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<DetalleDocEntrada> DetalleDocEntrada { get; set; } = new List<DetalleDocEntrada>();

    public virtual ICollection<DetalleDocSalida> DetalleDocSalida { get; set; } = new List<DetalleDocSalida>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Subcategoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual TipoPapel IdTipoPapelNavigation { get; set; } = null!;

    public virtual Kardex? Kardex { get; set; }

    public virtual ICollection<LibroAutor> LibroAutors { get; set; } = new List<LibroAutor>();

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
