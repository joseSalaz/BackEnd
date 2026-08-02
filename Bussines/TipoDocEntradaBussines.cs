<<<<<<< HEAD
using AutoMapper;
=======
﻿using AutoMapper;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using DBModel.DB;
using IBussines;
using IRepository;
using Models.RequestResponse;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using UnitOfWork;
=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4

namespace Bussines
{
    public class TipoDocEntradaBussines  : ITipoDocEntradaBussines
    {
        #region Declaracion de vcariables generales
        public readonly ITipoDocEntradaRepository _ITipoDocEntradaRepository = null;
        public readonly IMapper _Mapper;

        public TipoDocEntradaBussines()
        {
        }
        #endregion

<<<<<<< HEAD
        private readonly IUnitOfWork _unitOfWork;

    #region constructor 
        public TipoDocEntradaBussines(IMapper mapper, IUnitOfWork unitOfWork)
        {
      _unitOfWork = unitOfWork;
            _Mapper = mapper;
            _ITipoDocEntradaRepository = _unitOfWork.TiposDocEntrada;
=======
        #region constructor 
        public TipoDocEntradaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ITipoDocEntradaRepository = new TipoDocEntradaRepository();
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        }
        #endregion

        public TipoDocEntradaResponse Create(TipoDocEntradaRequest entity)
        {
            TipoDocEntrada au = _Mapper.Map<TipoDocEntrada>(entity);
            au = _ITipoDocEntradaRepository.Create(au);
            TipoDocEntradaResponse res = _Mapper.Map<TipoDocEntradaResponse>(au);
            return res;
        }

        public List<TipoDocEntradaResponse> CreateMultiple(List<TipoDocEntradaRequest> request)
        {
            List<TipoDocEntrada> au = _Mapper.Map<List<TipoDocEntrada>>(request);
            au = _ITipoDocEntradaRepository.InsertMultiple(au);
            List<TipoDocEntradaResponse> res = _Mapper.Map<List<TipoDocEntradaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ITipoDocEntradaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<TipoDocEntradaRequest> request)
        {
            List<TipoDocEntrada> au = _Mapper.Map<List<TipoDocEntrada>>(request);
            int cantidad = _ITipoDocEntradaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<TipoDocEntradaResponse> getAll()
        {
            List<TipoDocEntrada> lsl = _ITipoDocEntradaRepository.GetAll();
            List<TipoDocEntradaResponse> res = _Mapper.Map<List<TipoDocEntradaResponse>>(lsl);
            return res;
        }

        public List<TipoDocEntradaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public TipoDocEntradaResponse getById(object id)
        {
            TipoDocEntrada au = _ITipoDocEntradaRepository.GetById(id);
            TipoDocEntradaResponse res = _Mapper.Map<TipoDocEntradaResponse>(au);
            return res;
        }

        public TipoDocEntradaResponse Update(TipoDocEntradaRequest entity)
        {
            TipoDocEntrada au = _Mapper.Map<TipoDocEntrada>(entity);
            au = _ITipoDocEntradaRepository.Update(au);
            TipoDocEntradaResponse res = _Mapper.Map<TipoDocEntradaResponse>(au);
            return res;
        }

        public List<TipoDocEntradaResponse> UpdateMultiple(List<TipoDocEntradaRequest> request)
        {
            List<TipoDocEntrada> au = _Mapper.Map<List<TipoDocEntrada>>(request);
            au = _ITipoDocEntradaRepository.UpdateMultiple(au);
            List<TipoDocEntradaResponse> res = _Mapper.Map<List<TipoDocEntradaResponse>>(au);
            return res;
        }
    }
}
