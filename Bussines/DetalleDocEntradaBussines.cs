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
    public class DetalleDocEntradaBussines : IDetalleDocEntradaBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDetalleDocEntradaRepository _IDetalleDocEntradaRepository = null;
        public readonly IMapper _Mapper;

        public DetalleDocEntradaBussines()
        {
        }
        #endregion

<<<<<<< HEAD
        private readonly IUnitOfWork _unitOfWork;

    #region constructor 
        public DetalleDocEntradaBussines(IMapper mapper, IUnitOfWork unitOfWork)
        {
      _unitOfWork = unitOfWork;
            _Mapper = mapper;
            _IDetalleDocEntradaRepository = _unitOfWork.DetalleDocEntradas;
=======
        #region constructor 
        public DetalleDocEntradaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDetalleDocEntradaRepository = new DetalleDocEntradaRepository();
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        }
        #endregion

        public DetalleDocEntradaResponse Create(DetalleDocEntradaRequest entity)
        {
            DetalleDocEntrada au = _Mapper.Map<DetalleDocEntrada>(entity);
            au = _IDetalleDocEntradaRepository.Create(au);
            DetalleDocEntradaResponse res = _Mapper.Map<DetalleDocEntradaResponse>(au);
            return res;
        }

        public List<DetalleDocEntradaResponse> CreateMultiple(List<DetalleDocEntradaRequest> request)
        {
            List<DetalleDocEntrada> au = _Mapper.Map<List<DetalleDocEntrada>>(request);
            au = _IDetalleDocEntradaRepository.InsertMultiple(au);
            List<DetalleDocEntradaResponse> res = _Mapper.Map<List<DetalleDocEntradaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDetalleDocEntradaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DetalleDocEntradaRequest> request)
        {
            List<DetalleDocEntrada> au = _Mapper.Map<List<DetalleDocEntrada>>(request);
            int cantidad = _IDetalleDocEntradaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DetalleDocEntradaResponse> getAll()
        {
            List<DetalleDocEntrada> lsl = _IDetalleDocEntradaRepository.GetAll();
            List<DetalleDocEntradaResponse> res = _Mapper.Map<List<DetalleDocEntradaResponse>>(lsl);
            return res;
        }

        public List<DetalleDocEntradaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DetalleDocEntradaResponse getById(object id)
        {
            DetalleDocEntrada au = _IDetalleDocEntradaRepository.GetById(id);
            DetalleDocEntradaResponse res = _Mapper.Map<DetalleDocEntradaResponse>(au);
            return res;
        }

        public DetalleDocEntradaResponse Update(DetalleDocEntradaRequest entity)
        {
            DetalleDocEntrada au = _Mapper.Map<DetalleDocEntrada>(entity);
            au = _IDetalleDocEntradaRepository.Update(au);
            DetalleDocEntradaResponse res = _Mapper.Map<DetalleDocEntradaResponse>(au);
            return res;
        }

        public List<DetalleDocEntradaResponse> UpdateMultiple(List<DetalleDocEntradaRequest> request)
        {
            List<DetalleDocEntrada> au = _Mapper.Map<List<DetalleDocEntrada>>(request);
            au = _IDetalleDocEntradaRepository.UpdateMultiple(au);
            List<DetalleDocEntradaResponse> res = _Mapper.Map<List<DetalleDocEntradaResponse>>(au);
            return res;
        }
    }
}
