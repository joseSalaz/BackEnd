using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBModel.DB;

public partial class LibreriaSaberContext : DbContext
{
    public LibreriaSaberContext()
    {
    }

    public LibreriaSaberContext(DbContextOptions<LibreriaSaberContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Caja> Cajas { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DatosGenerale> DatosGenerales { get; set; }

    public virtual DbSet<DetalleDocEntrada> DetalleDocEntradas { get; set; }

    public virtual DbSet<DetalleDocSalida> DetalleDocSalidas { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<DocEntrada> DocEntradas { get; set; }

    public virtual DbSet<DocSalida> DocSalidas { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<EstadoPedidoImagene> EstadoPedidoImagenes { get; set; }

    public virtual DbSet<Kardex> Kardices { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibroAutor> LibroAutors { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<PublicoObjetivo> PublicoObjetivos { get; set; }

    public virtual DbSet<Subcategoria> Subcategorias { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoDocEntrada> TipoDocEntradas { get; set; }

    public virtual DbSet<TipoDocSalida> TipoDocSalidas { get; set; }

    public virtual DbSet<TipoPapel> TipoPapels { get; set; }

    public virtual DbSet<TipoProveedor> TipoProveedors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=JOSÉSALAZAR\\SQLEXPRESS;Initial Catalog=Libreria_Saber;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autor__0DC8163E6E12E91F");

            entity.ToTable("Autor");

            entity.Property(e => e.IdAutor).HasColumnName("Id_Autor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.IdCaja).HasName("PK__Caja__A7BC8D95A348834F");

            entity.ToTable("Caja");

            entity.Property(e => e.IdCaja).HasColumnName("Id_Caja");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IngresosACaja)
                .HasColumnType("money")
                .HasColumnName("Ingresos_a_CAja");
            entity.Property(e => e.RetiroDeCaja)
                .HasColumnType("money")
                .HasColumnName("Retiro_de_Caja");
            entity.Property(e => e.SaldoFinal)
                .HasColumnType("money")
                .HasColumnName("Saldo_Final");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("money")
                .HasColumnName("Saldo_Inicial");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CB9033496010002E");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Categoria1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Categoria");
        });

        modelBuilder.Entity<DatosGenerale>(entity =>
        {
            entity.HasKey(e => e.IdDatosGenerales).HasName("PK__DatosGen__015EFAAFF6E7D930");

            entity.Property(e => e.IdDatosGenerales).HasColumnName("Id_DatosGenerales");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Razon_Social");
            entity.Property(e => e.Ruc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Telefono_Contacto");
        });

        modelBuilder.Entity<DetalleDocEntrada>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDocEntrada).HasName("PK__DetalleD__8B1294F4A29CF311");

            entity.Property(e => e.IdDocEntrada).HasColumnName("IdDoc_Entrada");
            entity.Property(e => e.PorcentajeUtil).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PrecioCosto).HasColumnType("money");

            entity.HasOne(d => d.IdDocEntradaNavigation).WithMany(p => p.DetalleDocEntrada)
                .HasForeignKey(d => d.IdDocEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDocEntrada_DocEntrada");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.DetalleDocEntrada)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDocEntrada_Libro");
        });

        modelBuilder.Entity<DetalleDocSalida>(entity =>
        {
            entity.HasKey(e => e.IdDetalleSalida).HasName("PK__Detalle___B5F901B225CFDEEF");

            entity.ToTable("Detalle_Doc_Salidas");

            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDocSalidaNavigation).WithMany(p => p.DetalleDocSalida)
                .HasForeignKey(d => d.IdDocSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDocSalida_DocSalida");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.DetalleDocSalida)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDocSalida_Libro");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVentas).HasName("PK__Detalle___3AD94176C1FECF50");

            entity.ToTable("Detalle_Ventas");

            entity.Property(e => e.IdDetalleVentas).HasColumnName("id_detalleVentas");
            entity.Property(e => e.Estado)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdVentas).HasColumnName("id_Ventas");
            entity.Property(e => e.Importe).HasColumnType("money");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_Producto");
            entity.Property(e => e.PrecioUnit)
                .HasColumnType("money")
                .HasColumnName("precio_Unit");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVenta_Libro");

            entity.HasOne(d => d.IdVentasNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVentas)
                .HasConstraintName("FK_detalle_ventas_ventas");
        });

        modelBuilder.Entity<DocEntrada>(entity =>
        {
            entity.HasKey(e => e.IdDocEntrada).HasName("PK__Doc_Entr__A667F4C73330EB16");

            entity.ToTable("Doc_Entradas");

            entity.Property(e => e.IdDocEntrada).HasColumnName("IdDoc_Entrada");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");
            entity.Property(e => e.IdSucursal).HasColumnName("Id_Sucursal");
            entity.Property(e => e.NroDocEntrada)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.DocEntrada)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocEntrada_Proveedor");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.DocEntrada)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocEntrada_Sucursal");

            entity.HasOne(d => d.IdTipoDocEntradaNavigation).WithMany(p => p.DocEntrada)
                .HasForeignKey(d => d.IdTipoDocEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocEntrada_TipoDocEntrada");
        });

        modelBuilder.Entity<DocSalida>(entity =>
        {
            entity.HasKey(e => e.IdDocSalida).HasName("PK__DocSalid__A8C34201FDF179B3");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdSucursal).HasColumnName("Id_Sucursal");
            entity.Property(e => e.NroDocSalida).HasColumnName("Nro_Doc_salida");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.DocSalida)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocSalida_Sucursal");

            entity.HasOne(d => d.IdTipoDocSalidaNavigation).WithMany(p => p.DocSalida)
                .HasForeignKey(d => d.IdTipoDocSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocSalida_TipoDocSalida");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK__Estado_P__986B592371F9007B");

            entity.ToTable("Estado_Pedido");

            entity.Property(e => e.IdEstadoPedido).HasColumnName("id_estado_pedido");
            entity.Property(e => e.Comentario)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaEstado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdDetalleVentas).HasColumnName("id_detalleVentas");

            entity.HasOne(d => d.IdDetalleVentasNavigation).WithMany(p => p.EstadoPedidos)
                .HasForeignKey(d => d.IdDetalleVentas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadoPedido_DetalleVentas");
        });

        modelBuilder.Entity<EstadoPedidoImagene>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedidoImagen).HasName("PK__Estado_P__DE31F1F0470D8C35");

            entity.ToTable("Estado_Pedido_Imagenes");

            entity.Property(e => e.IdEstadoPedidoImagen).HasColumnName("id_estado_pedido_imagen");
            entity.Property(e => e.Estado).HasMaxLength(255);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdEstadoPedido).HasColumnName("id_estado_pedido");
            entity.Property(e => e.UrlImagen)
                .IsUnicode(false)
                .HasColumnName("URL_Imagen");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.EstadoPedidoImagenes)
                .HasForeignKey(d => d.IdEstadoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadoPedidoImagenes_EstadoPedido");
        });

        modelBuilder.Entity<Kardex>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Kardex__3E0B49AD434C48C9");

            entity.ToTable("Kardex");

            entity.Property(e => e.IdLibro).ValueGeneratedNever();
            entity.Property(e => e.CantidadEntrada).HasColumnName("Cantidad_Entrada");
            entity.Property(e => e.CantidadSalida).HasColumnName("Cantidad_Salida");
            entity.Property(e => e.IdSucursal).HasColumnName("Id_Sucursal");
            entity.Property(e => e.UltPrecioCosto).HasColumnType("money");

            entity.HasOne(d => d.IdLibroNavigation).WithOne(p => p.Kardex)
                .HasForeignKey<Kardex>(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_Libro");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Kardices)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_Sucursal");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Libro__3E0B49AD84725BB8");

            entity.ToTable("Libro");

            entity.Property(e => e.Condicion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");
            entity.Property(e => e.IdSubcategoria).HasColumnName("Id_Subcategoria");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Impresion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Tamanno)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TipoTapa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_Tapa");
            entity.Property(e => e.Titulo).IsUnicode(false);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_Proveedor");

            entity.HasOne(d => d.IdSubcategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdSubcategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Libro_SubCategoria");

            entity.HasOne(d => d.IdTipoPapelNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdTipoPapel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_TipoPapel");
        });

        modelBuilder.Entity<LibroAutor>(entity =>
        {
            entity.HasKey(e => e.IdLibroAutor).HasName("PK__LibroAut__6C6627617D4B18C8");

            entity.ToTable("LibroAutor");

            entity.Property(e => e.IdAutor).HasColumnName("Id_Autor");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.LibroAutors)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroAutor_Autor");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.LibroAutors)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroAutor_Libro");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__C95634AFC54BEA08");

            entity.ToTable("Persona");

            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numero_Documento");
            entity.Property(e => e.Sub)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sub");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_Documento");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => e.IdPrecios).HasName("PK__Precios__F69F1CD0BFCCDE3E");

            entity.Property(e => e.IdPrecios).HasColumnName("Id_Precios");
            entity.Property(e => e.IdPublicoObjetivo).HasColumnName("idPublicoObjetivo");
            entity.Property(e => e.PorcUtilidad)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Porc_Utilidad");
            entity.Property(e => e.PrecioVenta).HasColumnType("money");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Precios)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Precios_Libro");

            entity.HasOne(d => d.IdPublicoObjetivoNavigation).WithMany(p => p.Precios)
                .HasForeignKey(d => d.IdPublicoObjetivo)
                .HasConstraintName("FK_PublicoObjetivo_id");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__477B858E512D721A");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdTipoProveedor).HasColumnName("IdTipo_Proveedor");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Razon_Social");
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoProveedorNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.IdTipoProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor_TipoProveedor");
        });

        modelBuilder.Entity<PublicoObjetivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PublicoO__3213E83F308E023E");

            entity.ToTable("PublicoObjetivo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).IsUnicode(false);
        });

        modelBuilder.Entity<Subcategoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subcateg__3213E83F949A7BA8");

            entity.ToTable("subcategorias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Subcategoria)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__subcatego__id_ca__6383C8BA");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__02EDB3EAB5FFA5CD");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("Id_Sucursal");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocEntrada>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocEntrada).HasName("PK__TipoDocE__570697843153C09F");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocSalida>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocSalida).HasName("PK__TipoDocS__18DCE9BEE0AA0ABF");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPapel>(entity =>
        {
            entity.HasKey(e => e.IdTipoPapel).HasName("PK__TipoPape__18A06AAF0F64107F");

            entity.ToTable("TipoPapel");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoProveedor>(entity =>
        {
            entity.HasKey(e => e.IdTipoProveedor).HasName("PK__Tipo_Pro__B7282E35E31DFACD");

            entity.ToTable("Tipo_Proveedor");

            entity.Property(e => e.IdTipoProveedor).HasColumnName("IdTipo_Proveedor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__63C76BE2490653E9");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.NotificationToken).IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Persona");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVentas).HasName("PK__Ventas__464C581FB2637464");

            entity.Property(e => e.IdVentas).HasColumnName("Id_Ventas");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Venta");
            entity.Property(e => e.IdCaja).HasColumnName("Id_Caja");
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.NroComprobante)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TipoComprobante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo_Comprobante");
            entity.Property(e => e.TotalPrecio)
                .HasColumnType("money")
                .HasColumnName("Total_Precio");

            entity.HasOne(d => d.IdCajaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Caja");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Ventas_Personas");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
