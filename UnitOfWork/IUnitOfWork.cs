using IRepository;
using IRepositorio;

namespace UnitOfWork
{
    /// <summary>
    /// Unidad de trabajo (Unit of Work) para todo el sistema.
    ///
    /// Objetivo:
    ///  - Compartir UN SOLO DbContext entre todos los repositorios que participen en una
    ///    misma operación de negocio (antes cada repositorio creaba su propio contexto,
    ///    lo que hacía imposible una transacción real entre, por ejemplo, Venta + DetalleVenta
    ///    + Kardex + Caja).
    ///  - Exponer control explícito de transacción (Begin/Commit/Rollback) para operaciones
    ///    de negocio que tocan varias entidades y deben ser todo-o-nada.
    ///
    /// Se inyecta en la capa Bussines (nunca en Controllers) mediante DI, con ciclo de vida Scoped
    /// (una instancia por request), igual que el DbContext.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // ---- Repositorios expuestos (uno por entidad) ----
        IAutorRepository Autores { get; }
        ICajaRepository Cajas { get; }
        ICategoriaRepository Categorias { get; }
        IDatosGeneralesRepository DatosGenerales { get; }
        IDetalleDocEntradaRepository DetalleDocEntradas { get; }
        IDetalleDocSalidaRepository DetalleDocSalidas { get; }
        IDetalleVentaRepository DetalleVentas { get; }
        IDireccionRepository Direcciones { get; }
        IDocEntradaRepository DocEntradas { get; }
        IDocSalidaRepository DocSalidas { get; }
        IEstadoPedidoRepository EstadosPedido { get; }
        IEstadoPedidoImageneRepository EstadoPedidoImagenes { get; }
        IFavoritoRepository Favoritos { get; }
        IKardexRepository Kardex { get; }
        ILibroAutorRepository LibroAutores { get; }
        ILibroRepository Libros { get; }
        IPersonaRepository Personas { get; }
        IPrecioRepository Precios { get; }
        IProveedorRepository Proveedores { get; }
        IPublicoObjetivoRepository PublicosObjetivo { get; }
        ISubcategoriaRepository Subcategorias { get; }
        ISucursalRepository Sucursales { get; }
        ITipoDocEntradaRepository TiposDocEntrada { get; }
        ITipoDocSalidaRepository TiposDocSalida { get; }
        ITipoPapelRepository TiposPapel { get; }
        IUsuarioRepository Usuarios { get; }
        IVentaRepository Ventas { get; }

        /// <summary>Persiste los cambios pendientes en el contexto actual.</summary>
        int SaveChanges();

        /// <summary>Persiste los cambios pendientes en el contexto actual (async).</summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>Abre una transacción explícita a nivel de base de datos.</summary>
        Task BeginTransactionAsync();

        /// <summary>Confirma (SaveChanges + Commit) la transacción abierta.</summary>
        Task CommitAsync();

        /// <summary>Revierte todos los cambios realizados dentro de la transacción abierta.</summary>
        Task RollbackAsync();

        /// <summary>
        /// Ejecuta <paramref name="operation"/> dentro de una transacción con rollback automático
        /// si se lanza cualquier excepción. Es la forma recomendada de usar transacciones desde
        /// la capa Bussines: no requiere manejar Begin/Commit/Rollback manualmente.
        /// </summary>
        Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation);

        /// <summary>Igual que la sobrecarga genérica, para operaciones sin valor de retorno.</summary>
        Task ExecuteInTransactionAsync(Func<Task> operation);
    }
}
