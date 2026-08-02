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
  public class ProveedorController : ControllerBase
  {
    #region Declaracion de vcariables generales
    public readonly IProveedorBussines _IProveedorBussines = null;
    #endregion

    #region constructor 
    public ProveedorController(IProveedorBussines proveedorBussines)
    {
      _IProveedorBussines = proveedorBussines;
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
      List<ProveedorResponse> lsl = _IProveedorBussines.getAll();
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
      ProveedorResponse res = _IProveedorBussines.getById(id);
      return Ok(res);
    }

    /// <summary>
    /// Inserta un nuevo registro
    /// </summary>
    /// <param name="request">Registro a insertar</param>
    /// <returns>Retorna el registro insertado</returns>
    [HttpPost]
    public IActionResult Create([FromBody] ProveedorRequest request)
    {
      ProveedorResponse res = _IProveedorBussines.Create(request);
      return Ok(res);
    }

    /// <summary>
    /// Actualiza un registro
    /// </summary>
    /// <param name="entity">registro a actualizar</param>
    /// <returns>retorna el registro Actualiza</returns>
    [HttpPut]
    public IActionResult Update([FromBody] ProveedorRequest request)
    {
      ProveedorResponse res = _IProveedorBussines.Update(request);
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
      int res = _IProveedorBussines.Delete(id);
      return Ok(res);
    }

    [HttpGet("ProvvedorCategoria")]
    public async Task<IActionResult> getProveedorCategoria(int idCategoria, int? idSubcategoria)
    {
      var result = await _IProveedorBussines.getProveedorCategoria(idCategoria, idSubcategoria);
      return Ok(result);
    }
    #endregion
  }
}
