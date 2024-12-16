using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public int IdSubcategoria { get; set; }

    public int IdTipoPapel { get; set; }

    public int IdProveedor { get; set; }

    public string? Imagen { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleDocEntrada> DetalleDocEntrada { get; set; } = new List<DetalleDocEntrada>();
    [JsonIgnore]
    public virtual ICollection<DetalleDocSalida> DetalleDocSalida { get; set; } = new List<DetalleDocSalida>();
    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    [JsonIgnore]
    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Subcategoria IdSubcategoriaNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual TipoPapel IdTipoPapelNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Kardex? Kardex { get; set; }
    [JsonIgnore]
    public virtual ICollection<LibroAutor> LibroAutors { get; set; } = new List<LibroAutor>();
    [JsonIgnore]
    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();
}
