using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using UnitOfWork;

namespace API.Controllers
{
  [Route("[controller]")]
  [ApiController]

  //[Authorize]
  public class SubcategoriaController : ControllerBase
  {
    #region Declaracion de vcariables generales
    public readonly ISubcategoriaBussines _ISubcategoriaBussines = null;
    private readonly ILibroBussines _IlibroBussines;
    #endregion

    #region constructor 
    public SubcategoriaController(ILibroBussines libroBussines, ISubcategoriaBussines subcategoriaBussines)
    {
      _ISubcategoriaBussines = subcategoriaBussines;
      _IlibroBussines = libroBussines;
    }
    #endregion

    #region crud methods
    /// <summary>
    /// Retorna todos los registros
    /// </summary>
    /// <returns>Retorna todos los registros</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
      List<SubcategoriaResponse> lsl = _ISubcategoriaBussines.getAll();
      return Ok(lsl);
    }

    /// <summary>
    /// retorna el registro por Primary key
    /// </summary>
    /// <param name="id">PK</param>
    /// <returns>retorna el registro</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      SubcategoriaResponse res = _ISubcategoriaBussines.getById(id);
      return Ok(res);
    }

    /// <summary>
    /// Inserta un nuevo registro
    /// </summary>
    /// <param name="request">Registro a insertar</param>
    /// <returns>Retorna el registro insertado</returns>
    [HttpPost]
    public IActionResult Create([FromBody] SubcategoriaRequest request)
    {
      SubcategoriaResponse res = _ISubcategoriaBussines.Create(request);
      return Ok(res);
    }

    /// <summary>
    /// Actualiza un registro
    /// </summary>
    /// <param name="entity">registro a actualizar</param>
    /// <returns>retorna el registro Actualiza</returns>
    [HttpPut]
    public IActionResult Update([FromBody] SubcategoriaRequest request)
    {
      SubcategoriaResponse res = _ISubcategoriaBussines.Update(request);
      return Ok(res);
    }

    /// <summary>
    /// Elimina un registro
    /// </summary>
    /// <param name="id">Valor del PK</param>
    /// <returns>Cantidad de registros afectados</returns>
    [HttpDelete("{id}")]
    public IActionResult delete(int id)
    {
      int res = _ISubcategoriaBussines.Delete(id);
      return Ok(res);
    }
    [HttpGet("librosbysubcategoria/{subcategoriaId}")]
    public async Task<IActionResult> GetLibrosBySubcategoria(int subcategoriaId)
    {

      var libros = await _ISubcategoriaBussines
          .GetLibrosCatalogoBySubcategoria(subcategoriaId);


      return Ok(new
      {
        success = true,
        data = libros
      });

    }

    [HttpGet("filtrar")]
    public async Task<IActionResult> FiltrarSubcategorias(
        [FromQuery] int? categoriaId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
      var (subcategorias, totalItems) = await _ISubcategoriaBussines.FiltrarSubcategoriasAsync(categoriaId, page, pageSize);

      var response = new
      {
        subcategorias,
        totalItems,
        totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
        currentPage = page
      };

      return Ok(response);
    }


    #endregion
  }
}
