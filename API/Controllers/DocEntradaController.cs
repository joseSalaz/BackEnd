<<<<<<< HEAD
using AutoMapper;
=======
﻿using AutoMapper;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
<<<<<<< HEAD
using UnitOfWork;

namespace API.Controllers
{
  [Route("[controller]")]
  [ApiController]

  //[Authorize]
  public class DocEntradaController : ControllerBase
  {
    #region Declaracion de vcariables generales
    public readonly IDocEntradaBussines _IDocEntradaBussines = null;
    #endregion

    #region constructor 
    public DocEntradaController(IDocEntradaBussines docEntradaBussines)
    {
      _IDocEntradaBussines = docEntradaBussines;
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
      List<DocEntradaResponse> lsl = _IDocEntradaBussines.getAll();
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
      DocEntradaResponse res = _IDocEntradaBussines.getById(id);
      return Ok(res);
    }

    /// <summary>
    /// Inserta un nuevo registro
    /// </summary>
    /// <param name="request">Registro a insertar</param>
    /// <returns>Retorna el registro insertado</returns>
    [HttpPost]
    public IActionResult Create([FromBody] DocEntradaRequest request)
    {
      DocEntradaResponse res = _IDocEntradaBussines.Create(request);
      return Ok(res);
    }

    /// <summary>
    /// Actualiza un registro
    /// </summary>
    /// <param name="entity">registro a actualizar</param>
    /// <returns>retorna el registro Actualiza</returns>
    [HttpPut]
    public IActionResult Update([FromBody] DocEntradaRequest request)
    {
      DocEntradaResponse res = _IDocEntradaBussines.Update(request);
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
      int res = _IDocEntradaBussines.Delete(id);
      return Ok(res);
    }
    #endregion
  }
=======

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    //[Authorize]
    public class DocEntradaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IDocEntradaBussines _IDocEntradaBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public DocEntradaController(IMapper mapper)
        {
            _Mapper = mapper;
            _IDocEntradaBussines = new DocEntradaBussines(_Mapper);
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
            List<DocEntradaResponse> lsl = _IDocEntradaBussines.getAll();
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
            DocEntradaResponse res = _IDocEntradaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] DocEntradaRequest request)
        {
            DocEntradaResponse res = _IDocEntradaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] DocEntradaRequest request)
        {
            DocEntradaResponse res = _IDocEntradaBussines.Update(request);
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
            int res = _IDocEntradaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
