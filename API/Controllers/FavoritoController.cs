using AutoMapper;
using Bussines;
using DBModel.DB;
using IBussines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IFavoritoBussines _IFavoritoBussines = null;
<<<<<<< HEAD
        #endregion

        #region constructor
        public FavoritoController(IFavoritoBussines favoritoBussines)
        {
            _IFavoritoBussines = favoritoBussines;
=======
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public FavoritoController(IMapper mapper)
        {
            _Mapper = mapper;
            _IFavoritoBussines = new FavoritoBussines(_Mapper);
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        }
        #endregion

        [HttpPost]
        public IActionResult Create([FromBody] FavoritoRequest request)
        {
            FavoritoResponse res = _IFavoritoBussines.Create(request);
            return Ok(res);
        }

        [HttpDelete("{idPersona}/{idLibro}")]
        public async Task<IActionResult> Delete(int idPersona, int idLibro)
        {
            int res = await _IFavoritoBussines.DeleteByPersonaAndLibroAsync(idPersona, idLibro);

            if (res > 0)
            {
                return Ok(res);
            }

            return BadRequest("No se pudo eliminar el favorito o no existe.");
        }

        [HttpGet("persona/{idPersona}")]
        public async Task<IActionResult> GetByUsuario(int idPersona)
        {
            List<FavoritoResponse> res = await _IFavoritoBussines.ObtenerFavoritosPorUsuarioAsync(idPersona);
            return Ok(res);
        }

        [HttpGet("verificar")]
        public async Task<IActionResult> VerificarEsFavorito([FromQuery] int idPersona, [FromQuery] int idLibro)
        {
            bool esFavorito = await _IFavoritoBussines.VerificarEsFavoritoAsync(idPersona, idLibro);
            return Ok(esFavorito);
        }

    }
}
