using DBModel.DB;
using IRepository;
using IRepositorio;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork
{
  /// <summary>
  /// Implementación de <see cref="IUnitOfWork"/>. Recibe el <see cref="LibreriaSaberContext"/>
  /// por DI (registrado como Scoped en Program.cs) y crea los repositorios concretos pasándoles
  /// ese MISMO contexto, para que todos compartan conexión y transacción.
  /// Los repositorios se crean de forma perezosa (solo la primera vez que se usan).
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly LibreriaSaberContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    private IAutorRepository? _autores;
    private ICajaRepository? _cajas;
    private ICategoriaRepository? _categorias;
    private IDatosGeneralesRepository? _datosGenerales;
    private IDetalleDocEntradaRepository? _detalleDocEntradas;
    private IDetalleDocSalidaRepository? _detalleDocSalidas;
    private IDetalleVentaRepository? _detalleVentas;
    private IDireccionRepository? _direcciones;
    private IDocEntradaRepository? _docEntradas;
    private IDocSalidaRepository? _docSalidas;
    private IEstadoPedidoRepository? _estadosPedido;
    private IEstadoPedidoImageneRepository? _estadoPedidoImagenes;
    private IFavoritoRepository? _favoritos;
    private IKardexRepository? _kardex;
    private ILibroAutorRepository? _libroAutores;
    private ILibroRepository? _libros;
    private IPersonaRepository? _personas;
    private IPrecioRepository? _precios;
    private IProveedorRepository? _proveedores;
    private IPublicoObjetivoRepository? _publicosObjetivo;
    private ISubcategoriaRepository? _subcategorias;
    private ISucursalRepository? _sucursales;
    private ITipoDocEntradaRepository? _tiposDocEntrada;
    private ITipoDocSalidaRepository? _tiposDocSalida;
    private ITipoPapelRepository? _tiposPapel;
    private IUsuarioRepository? _usuarios;
    private IVentaRepository? _ventas;

    public UnitOfWork(LibreriaSaberContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IAutorRepository Autores => _autores ??= new AutorRepository(_context);
    public ICajaRepository Cajas => _cajas ??= new CajaRepository(_context);
    public ICategoriaRepository Categorias => _categorias ??= new CategoriaRepository(_context);
    public IDatosGeneralesRepository DatosGenerales => _datosGenerales ??= new DatosGeneralesRepository(_context);
    public IDetalleDocEntradaRepository DetalleDocEntradas => _detalleDocEntradas ??= new DetalleDocEntradaRepository(_context);
    public IDetalleDocSalidaRepository DetalleDocSalidas => _detalleDocSalidas ??= new DetalleDocSalidaRepository(_context);
    public IDetalleVentaRepository DetalleVentas => _detalleVentas ??= new DetalleVentaRepository(_context);
    public IDireccionRepository Direcciones => _direcciones ??= new DireccionRepository(_context);
    public IDocEntradaRepository DocEntradas => _docEntradas ??= new DocEntradaRepository(_context);
    public IDocSalidaRepository DocSalidas => _docSalidas ??= new DocSalidaRepository(_context);
    public IEstadoPedidoRepository EstadosPedido => _estadosPedido ??= new EstadoPedidoRepository(_context);
    public IEstadoPedidoImageneRepository EstadoPedidoImagenes => _estadoPedidoImagenes ??= new EstadoPedidoImageneRepository(_context);
    public IFavoritoRepository Favoritos => _favoritos ??= new FavoritoRepository(_context);
    public IKardexRepository Kardex => _kardex ??= new KardexRepository(_context);
    public ILibroAutorRepository LibroAutores => _libroAutores ??= new LibroAutorRepository(_context);
    public ILibroRepository Libros => _libros ??= new LibroRepository(_context);
    public IPersonaRepository Personas => _personas ??= new PersonaRepository(_context);
    public IPrecioRepository Precios => _precios ??= new PrecioRepository(_context);
    public IProveedorRepository Proveedores => _proveedores ??= new ProveedorRepository(_context);
    public IPublicoObjetivoRepository PublicosObjetivo => _publicosObjetivo ??= new PublicoObjetivoRepository(_context);
    public ISubcategoriaRepository Subcategorias => _subcategorias ??= new SubcategoriaRepository(_context);
    public ISucursalRepository Sucursales => _sucursales ??= new SucursalRepository(_context);
    public ITipoDocEntradaRepository TiposDocEntrada => _tiposDocEntrada ??= new TipoDocEntradaRepository(_context);
    public ITipoDocSalidaRepository TiposDocSalida => _tiposDocSalida ??= new TipoDocSalidaRepository(_context);
    public ITipoPapelRepository TiposPapel => _tiposPapel ??= new TipoPapelRepository(_context);
    public IUsuarioRepository Usuarios => _usuarios ??= new UsuarioRepository(_context);
    public IVentaRepository Ventas => _ventas ??= new VentaRepository(_context);

    public int SaveChanges() => _context.SaveChanges();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync()
    {
      _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
      try
      {
        await _context.SaveChangesAsync();
        if (_transaction != null)
        {
          await _transaction.CommitAsync();
        }
      }
      catch
      {
        await RollbackAsync();
        throw;
      }
      finally
      {
        if (_transaction != null)
        {
          await _transaction.DisposeAsync();
          _transaction = null;
        }
      }
    }

    public async Task RollbackAsync()
    {
      if (_transaction != null)
      {
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
      }
    }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation)
    {
      // CreateExecutionStrategy es la forma recomendada por EF Core de combinar
      // transacciones explícitas con reintentos de conexión (p. ej. EnableRetryOnFailure).
      var strategy = _context.Database.CreateExecutionStrategy();

      return await strategy.ExecuteAsync(async () =>
      {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
          var result = await operation();
          await _context.SaveChangesAsync();
          await transaction.CommitAsync();
          return result;
        }
        catch
        {
          await transaction.RollbackAsync();
          throw;
        }
      });
    }

    public Task ExecuteInTransactionAsync(Func<Task> operation)
        => ExecuteInTransactionAsync(async () =>
        {
          await operation();
          return true;
        });

    public void Dispose()
    {
      if (_disposed) return;
      _transaction?.Dispose();
      _context.Dispose();
      _disposed = true;
      GC.SuppressFinalize(this);
    }
  }
}
