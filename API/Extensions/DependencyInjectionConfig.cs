using AutoMapper;
using Bussines;
using Bussnies;
using IBussines;
using IBussnies;
using IRepository;
using IRepositorio;
using IService;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;
using UnitOfWork;
using UtilMapper;

namespace API.Extensions
{
  public static class DependencyInjectionConfig
  {
    public static IServiceCollection AddProyectDependencies(this IServiceCollection services)
    {
      // ==== AutoMapper (resuelve IMapper inyectado en Bussines) ====
      services.AddAutoMapper(typeof(AutoMapperProfiles));

      // ==== Unit of Work (un solo contexto compartido por request) ====
      services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

      // ==== Capa de Negocio (Bussines) ====
      // Nota: las clases viven en distintos namespaces por typos historicos
      // (Bussines.AuthBussnies, Bussnies.UsuarioBussnies, etc.).
      services.AddScoped<IAuthBussines, AuthBussnies>();
      services.AddScoped<IAutorBussines, AutorBussines>();
      services.AddScoped<ICajaBussines, CajaBussines>();
      services.AddScoped<ICategoriaBussines, CategoriaBussines>();
      services.AddScoped<IDatosGeneralesBussines, DatosGeneralesBussines>();
      services.AddScoped<IDetalleDocEntradaBussines, DetalleDocEntradaBussines>();
      services.AddScoped<IDetalleDocSalidaBussines, DetalleDocSalidaBussines>();
      services.AddScoped<IDetalleVentaBussines, DetalleVentaBussines>();
      services.AddScoped<IDireccionBussines, DireccionBussines>();
      services.AddScoped<IDocEntradaBussines, DocEntradaBussines>();
      services.AddScoped<IDocSalidaBussines, DocSalidaBussines>();
      services.AddScoped<IEstadoPedidoBussines, EstadoPedidoBussines>();
      services.AddScoped<IEstadoPedidoImageneBussines, EstadoPedidoImageneBussines>();
      services.AddScoped<IFavoritoBussines, FavoritoBussines>();
      services.AddScoped<IKardexBussines, KardexBussines>();
      services.AddScoped<ILibroAutorBussines, LibroAutorBussines>();
      services.AddScoped<ILibroBussines, LibroBussines>();
      services.AddScoped<IPersonaBussines, PersonaBussines>();
      services.AddScoped<IPrecioBussines, PrecioBussines>();
      services.AddScoped<IProveedorBussines, ProveedorBussines>();
      services.AddScoped<IPublicoObjetivoBussines, PublicoObjetivoBussines>();
      services.AddScoped<ISubcategoriaBussines, SubcategoriaBussines>();
      services.AddScoped<ISucursalBussines, SucursalBussines>();
      services.AddScoped<ITipoDocEntradaBussines, TipoDocEntradaBussines>();
      services.AddScoped<ITipoDocSalidaBussines, TipoDocSalidaBussines>();
      services.AddScoped<ITipoPapelBussines, TipoPapelBussines>();
      services.AddScoped<IUsuarioBussnies, Bussnies.UsuarioBussnies>();
      services.AddScoped<IVentaBussines, VentaBussines>();
      // IProcesarPagoBussines: omitido, la interfaz y la clase son 'internal' (no inyectables).

      // ==== Capa de Datos (Repositories) ====
      services.AddScoped<IAutorRepository, AutorRepository>();
      services.AddScoped<ICajaRepository, CajaRepository>();
      services.AddScoped<ICategoriaRepository, CategoriaRepository>();
      services.AddScoped<IDatosGeneralesRepository, DatosGeneralesRepository>();
      services.AddScoped<IDetalleDocEntradaRepository, DetalleDocEntradaRepository>();
      services.AddScoped<IDetalleDocSalidaRepository, DetalleDocSalidaRepository>();
      services.AddScoped<IDetalleVentaRepository, DetalleVentaRepository>();
      services.AddScoped<IDireccionRepository, DireccionRepository>();
      services.AddScoped<IDocEntradaRepository, DocEntradaRepository>();
      services.AddScoped<IDocSalidaRepository, DocSalidaRepository>();
      services.AddScoped<IEstadoPedidoImageneRepository, EstadoPedidoImageneRepository>();
      services.AddScoped<IEstadoPedidoRepository, EstadoPedidoRepository>();
      services.AddScoped<IFavoritoRepository, FavoritoRepository>();
      services.AddScoped<IKardexRepository, KardexRepository>();
      services.AddScoped<ILibroAutorRepository, LibroAutorRepository>();
      services.AddScoped<ILibroRepository, LibroRepository>();
      services.AddScoped<IPersonaRepository, PersonaRepository>();
      services.AddScoped<IPrecioRepository, PrecioRepository>();
      services.AddScoped<IProveedorRepository, ProveedorRepository>();
      services.AddScoped<IPublicoObjetivoRepository, PublicoObjetivoRepository>();
      services.AddScoped<ISubcategoriaRepository, SubcategoriaRepository>();
      services.AddScoped<ISucursalRepository, SucursalRepository>();
      services.AddScoped<ITipoDocEntradaRepository, TipoDocEntradaRepository>();
      services.AddScoped<ITipoDocSalidaRepository, TipoDocSalidaRepository>();
      services.AddScoped<ITipoPapelRepository, TipoPapelRepository>();
      services.AddScoped<IUsuarioRepository, UsuarioRepository>();
      services.AddScoped<IVentaRepository, VentaRepository>();

      // ==== Servicios externos / utilities ====
      services.AddScoped<IApisPaypalServices, ApisPaypalServices>();
      services.AddScoped<IApisPeruServices, ApisPeruServices>();
      services.AddScoped<IAuthenticationService, AuthenticationService>();
      services.AddScoped<IAzureComputerVisionService, AzureComputerVisionService>();
      services.AddScoped<ICriptoService, CriptoService>();
      services.AddScoped<IEmailService, EmailService>();
      services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
      services.AddScoped<IOrderMesageFirebase, OrderMesageFirebase>();
      services.AddScoped<IPaymentService, MercadoPagoService>();
      // IAzureStorage / AzureStorage: omitido, el archivo esta 100% comentado (stub).

      return services;
    }
  }
}
