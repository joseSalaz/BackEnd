<<<<<<< HEAD
using AutoMapper;
=======
﻿using AutoMapper;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using IBussines;
using IRepository;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModel.DB;
using Repository;
<<<<<<< HEAD
using UnitOfWork;
namespace Bussines
{
  public class FavoritoBussines : IFavoritoBussines
  {
    #region Declaracion de vcariables generales
    public readonly IFavoritoRepository _IFavoritoRepository = null;
    public readonly IMapper _Mapper;
    public readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor 
    public FavoritoBussines(IMapper mapper, IUnitOfWork unitOfWork)
    {
      _Mapper = mapper;
      _unitOfWork = unitOfWork;
      _IFavoritoRepository = _unitOfWork.Favoritos;
    }
    #endregion

    #region Controladores del repositorio 
    public async Task<List<FavoritoResponse>> ObtenerFavoritosPorUsuarioAsync(int idPersona)
    {
      List<Favorito> lista = await _IFavoritoRepository.GetFavoritosByUsuarioAsync(idPersona);
      return _Mapper.Map<List<FavoritoResponse>>(lista);
    }

    public async Task<bool> VerificarEsFavoritoAsync(int idPersona, int idLibro)
    {
      return await _IFavoritoRepository.EsFavoritoAsync(idPersona, idLibro);
    }
    public async Task<int> DeleteByPersonaAndLibroAsync(int idPersona, int idLibro)
    {
      return await _IFavoritoRepository.DeleteByPersonaAndLibroAsync(idPersona, idLibro);
    }
    #endregion

    #region Controladores genericos 
    public FavoritoResponse Create(FavoritoRequest entity)
    {
      Favorito fav = _Mapper.Map<Favorito>(entity);
      fav = _IFavoritoRepository.Create(fav);
      FavoritoResponse res = _Mapper.Map<FavoritoResponse>(fav);
      return res;
    }

    public int Delete(object id)
    {
      return _IFavoritoRepository.Delete(id);
    }

    public List<FavoritoResponse> getAll()
    {
      throw new NotImplementedException();
    }

    public FavoritoResponse getById(object id)
    {
      throw new NotImplementedException();
    }

    public FavoritoResponse Update(FavoritoRequest entity)
    {
      throw new NotImplementedException();
    }

    public int deleteMultipleItems(List<FavoritoRequest> request)
    {
      throw new NotImplementedException();
    }

    public List<FavoritoResponse> CreateMultiple(List<FavoritoRequest> request)
    {
      throw new NotImplementedException();
    }

    public List<FavoritoResponse> UpdateMultiple(List<FavoritoRequest> request)
    {
      throw new NotImplementedException();
    }

    public List<FavoritoResponse> getAutoComplete(string query)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      _unitOfWork?.Dispose();
    }
    #endregion

  }
=======
namespace Bussines
{
    public class FavoritoBussines : IFavoritoBussines
    {
        #region Declaracion de vcariables generales
        public readonly IFavoritoRepository _IFavoritoRepository = null;
        public readonly IMapper _Mapper;
        #endregion

        #region Constructor 
        public FavoritoBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IFavoritoRepository = new FavoritoRepository();
        }
        #endregion

        #region Controladores del repositorio 
        public async Task<List<FavoritoResponse>> ObtenerFavoritosPorUsuarioAsync(int idPersona)
        {
            List<Favorito> lista = await _IFavoritoRepository.GetFavoritosByUsuarioAsync(idPersona);
            return _Mapper.Map<List<FavoritoResponse>>(lista);
        }

        public async Task<bool> VerificarEsFavoritoAsync(int idPersona, int idLibro)
        {
            return await _IFavoritoRepository.EsFavoritoAsync(idPersona, idLibro);
        }
        public async Task<int> DeleteByPersonaAndLibroAsync(int idPersona, int idLibro)
        {
            return await _IFavoritoRepository.DeleteByPersonaAndLibroAsync(idPersona, idLibro);
        }
        #endregion

        #region Controladores genericos 
        public FavoritoResponse Create(FavoritoRequest entity)
        {
            Favorito fav = _Mapper.Map<Favorito>(entity);
            fav = _IFavoritoRepository.Create(fav);
            FavoritoResponse res = _Mapper.Map<FavoritoResponse>(fav);
            return res;
        }

        public int Delete(object id)
        {
            return _IFavoritoRepository.Delete(id);
        }

        public List<FavoritoResponse> getAll()
        {
            throw new NotImplementedException();
        }

        public FavoritoResponse getById(object id)
        {
            throw new NotImplementedException();
        }

        public FavoritoResponse Update(FavoritoRequest entity)
        {
            throw new NotImplementedException();
        }

        public int deleteMultipleItems(List<FavoritoRequest> request)
        {
            throw new NotImplementedException();
        }

        public List<FavoritoResponse> CreateMultiple(List<FavoritoRequest> request)
        {
            throw new NotImplementedException();
        }

        public List<FavoritoResponse> UpdateMultiple(List<FavoritoRequest> request)
        {
            throw new NotImplementedException();
        }

        public List<FavoritoResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
