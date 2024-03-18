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

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DatosGenerale> DatosGenerales { get; set; }

    public virtual DbSet<DetalleDocEntrada> DetalleDocEntradas { get; set; }

    public virtual DbSet<DetalleDocSalida> DetalleDocSalidas { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<DocEntrada> DocEntradas { get; set; }

    public virtual DbSet<DocSalida> DocSalidas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Kardex> Kardices { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibroAutor> LibroAutors { get; set; }

    public virtual DbSet<LibroGenero> LibroGeneros { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoDocEntrada> TipoDocEntradas { get; set; }

    public virtual DbSet<TipoDocSalida> TipoDocSalidas { get; set; }

    public virtual DbSet<TipoPapel> TipoPapels { get; set; }

    public virtual DbSet<TipoProveedor> TipoProveedors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CVPFOKO;Initial Catalog=Libreria_Saber;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autor__0DC8163E94F100C0");

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
            entity.HasKey(e => e.IdCaja).HasName("PK__Caja__A7BC8D9535099879");

            entity.ToTable("Caja");

            entity.Property(e => e.IdCaja).HasColumnName("Id_Caja");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdVentas).HasColumnName("Id_Ventas");
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

            entity.HasOne(d => d.IdVentasNavigation).WithMany(p => p.Cajas)
                .HasForeignKey(d => d.IdVentas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Caja_Ventas");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__CB903349F1E1085E");

            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.Categoria1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__3DD0A8CBE840B3FD");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.CodigoCliente).HasColumnName("Codigo_Cliente");
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Persona");
        });

        modelBuilder.Entity<DatosGenerale>(entity =>
        {
            entity.HasKey(e => e.IdDatosGenerales).HasName("PK__DatosGen__015EFAAFC594F681");

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
            entity.HasKey(e => e.IdDetalleDocEntrada).HasName("PK__DetalleD__8B1294F4A14645B8");

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
            entity.HasKey(e => e.IdDetalleSalida).HasName("PK__Detalle___B5F901B226E8F97B");

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
            entity.HasKey(e => e.IdVentas).HasName("PK__Detalle___464C581FB95CC752");

            entity.ToTable("Detalle_Ventas");

            entity.Property(e => e.IdVentas)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id_Ventas");
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

            entity.HasOne(d => d.IdVentasNavigation).WithOne(p => p.DetalleVenta)
                .HasForeignKey<DetalleVenta>(d => d.IdVentas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVenta_Ventas");
        });

        modelBuilder.Entity<DocEntrada>(entity =>
        {
            entity.HasKey(e => e.IdDocEntrada).HasName("PK__Doc_Entr__A667F4C7B1A19D1E");

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
            entity.HasKey(e => e.IdDocSalida).HasName("PK__DocSalid__A8C3420170B39BE6");

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

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988E7424D62");

            entity.ToTable("Genero");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kardex>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Kardex__3E0B49AD02C1C096");

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
            entity.HasKey(e => e.IdLibro).HasName("PK__Libro__3E0B49AD67A4FDBF");

            entity.ToTable("Libro");

            entity.Property(e => e.Condicion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_Proveedor");
            entity.Property(e => e.Impresion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Isbn).HasColumnName("ISBN");
            entity.Property(e => e.RutaImagen).IsUnicode(false);
            entity.Property(e => e.Tamanno)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TipoTapa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_Tapa");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_Categoria");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_Proveedor");

            entity.HasOne(d => d.IdTipoPapelNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdTipoPapel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libro_TipoPapel");
        });

        modelBuilder.Entity<LibroAutor>(entity =>
        {
            entity.HasKey(e => e.IdLibroAutor).HasName("PK__LibroAut__6C662761A876682F");

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

        modelBuilder.Entity<LibroGenero>(entity =>
        {
            entity.HasKey(e => e.IdLibroGenero).HasName("PK__Libro_Ge__E1DAB7EF3FF6DFD0");

            entity.ToTable("Libro_Genero");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.LibroGeneros)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroGenero_Genero");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.LibroGeneros)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibroGenero_Libro");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__C95634AF01949CD8");

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
            entity.HasKey(e => e.IdPrecios).HasName("PK__Precios__F69F1CD05F4FED65");

            entity.Property(e => e.IdPrecios).HasColumnName("Id_Precios");
            entity.Property(e => e.PorcUtilidad)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Porc_Utilidad");
            entity.Property(e => e.PrecioVenta).HasColumnType("money");
            entity.Property(e => e.PublicoObjetivo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Publico_Objetivo");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Precios)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Precios_Libro");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__477B858E86FCD28D");

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

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__02EDB3EA9BAAF859");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("Id_Sucursal");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocEntrada>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocEntrada).HasName("PK__TipoDocE__570697845AE32E3A");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDocSalida>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocSalida).HasName("PK__TipoDocS__18DCE9BE01784398");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoPapel>(entity =>
        {
            entity.HasKey(e => e.IdTipoPapel).HasName("PK__TipoPape__18A06AAFBAAC7B64");

            entity.ToTable("TipoPapel");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoProveedor>(entity =>
        {
            entity.HasKey(e => e.IdTipoProveedor).HasName("PK__Tipo_Pro__B7282E35D1377475");

            entity.ToTable("Tipo_Proveedor");

            entity.Property(e => e.IdTipoProveedor).HasColumnName("IdTipo_Proveedor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__63C76BE22D7F2DA4");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdPersona).HasColumnName("Id_Persona");
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
            entity.HasKey(e => e.IdVentas).HasName("PK__Ventas__464C581FB0B13A78");

            entity.Property(e => e.IdVentas).HasColumnName("Id_Ventas");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Venta");
            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
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

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Cliente");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Libro");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
