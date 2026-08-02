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
  public class SucursalController : ControllerBase
  {
    #region Declaracion de vcariables generales
    public readonly ISucursalBussines _ISucursalBussines = null;
    #endregion

    #region constructor 
    public SucursalController(ISucursalBussines sucursalBussines)
    {
      _ISucursalBussines = sucursalBussines;
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
      List<SucursalResponse> lsl = _ISucursalBussines.getAll();
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
      SucursalResponse res = _ISucursalBussines.getById(id);
      return Ok(res);
    }

    /// <summary>
    /// Inserta un nuevo registro
    /// </summary>
    /// <param name="request">Registro a insertar</param>
    /// <returns>Retorna el registro insertado</returns>
    [HttpPost]
    public IActionResult Create([FromBody] SucursalRequest request)
    {
      SucursalResponse res = _ISucursalBussines.Create(request);
      return Ok(res);
    }

    /// <summary>
    /// Actualiza un registro
    /// </summary>
    /// <param name="entity">registro a actualizar</param>
    /// <returns>retorna el registro Actualiza</returns>
    [HttpPut]
    public IActionResult Update([FromBody] SucursalRequest request)
    {
      SucursalResponse res = _ISucursalBussines.Update(request);
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
      int res = _ISucursalBussines.Delete(id);
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
    public class SucursalController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ISucursalBussines _ISucursalBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public SucursalController(IMapper mapper)
        {
            _Mapper = mapper;
            _ISucursalBussines = new SucursalBussines(_Mapper);
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
            List<SucursalResponse> lsl = _ISucursalBussines.getAll();
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
            SucursalResponse res = _ISucursalBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] SucursalRequest request)
        {
            SucursalResponse res = _ISucursalBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] SucursalRequest request)
        {
            SucursalResponse res = _ISucursalBussines.Update(request);
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
            int res = _ISucursalBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
