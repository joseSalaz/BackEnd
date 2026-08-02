using DBModel.DB;
using DBModel.Response;
using IService;
using Microsoft.AspNetCore.Http;
using Models.RequestResponse.libro;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;
using static Models.RequestResponse.FiltroLibro;



namespace IBussines
{
  public interface ILibroBussines : ICRUDBussnies<LibroRequest, LibroResponse>
  {
    //Task<LibroResponse> CreateWithImage(LibroRequest entity, IFormFile imageFile);
    Task<LibroResponse> CreateWithImageFirebase(LibroconautorRequest entity, IFormFile imageFile, decimal precioVenta, int stock);
    Task<List<Libro>> GetLibrosByIds(List<int> ids);
    Task<LibroResponse> GetByIdAsync(object id);
    Task<Libro> ObtenerLibroConPreciosYPublicoObjetivo(int libroId);
    Task<Libro> ObtenerLibroCompletoPorIds(Libro libroConIds);
    Task<List<Precio>> GetPreciosByLibroId(int libroId);
    Task<Kardex> GetKardexByLibroId(int libroId);
    Task<(List<LibroResponse>, int)> GetLibrosPaginados(int page, int pageSize);
    Task<List<LibroResponse>> filtroComplete(string query);
    Task<LibroResponse> UpdateLib(LibroconautorRequest entity, IFormFile? imageFile, decimal precioVenta, int stock);
    Task<bool> CambiarEstadoLibro(int libroId);
    Task<(List<Libro>, int)> FiltrarLibrosAsync(bool? estado, string titulo, int page, int pageSize);
    Task<List<LibroDataResponse>> getLibroAutor();
    Task<List<LibroFiltroResponse>> FiltrarLibros(FiltroLibroRequest request);


    /// <summary>
    /// Analiza una imagen subida (portada de libro) usando Azure Computer Vision y devuelve
    /// las categorías, tags, descripción, objetos, color y detección de contenido adulto
    /// detectados por el servicio. El controller solo pasa el IFormFile; el Bussines se
    /// encarga de abrir el stream y orquestar el servicio externo.
    /// </summary>
    Task<Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.ImageAnalysis> AnalizarImagenAsync(IFormFile archivo);

    Task<IEnumerable<LibroCatalogoDTO>> ObtenerCatalogoOptimizadoAsync();
    Task<List<Libro>> GetLibrosConPreciosByIds(List<int> ids);
    Task<List<LibroCatalogo>> GetLibrosCatalogoByIds(List<int> ids);

  }
}
