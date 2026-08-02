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
    public class DatosGeneralesBussines: IDatosGeneralesBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDatosGeneralesRepository _IDatosGeneraleRepository = null;
        public readonly IMapper _Mapper;

        public DatosGeneralesBussines()
        {
        }
        #endregion

<<<<<<< HEAD
        private readonly IUnitOfWork _unitOfWork;

    #region constructor 
        public DatosGeneralesBussines(IMapper mapper, IUnitOfWork unitOfWork)
        {
      _unitOfWork = unitOfWork;
            _Mapper = mapper;
            _IDatosGeneraleRepository = _unitOfWork.DatosGenerales;
=======
        #region constructor 
        public DatosGeneralesBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDatosGeneraleRepository = new DatosGeneralesRepository();
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        }
        #endregion

        public DatosGeneraleResponse Create(DatosGeneraleRequest entity)
        {
            DatosGenerale au = _Mapper.Map<DatosGenerale>(entity);
            au = _IDatosGeneraleRepository.Create(au);
            DatosGeneraleResponse res = _Mapper.Map<DatosGeneraleResponse>(au);
            return res;
        }

        public List<DatosGeneraleResponse> CreateMultiple(List<DatosGeneraleRequest> request)
        {
            List<DatosGenerale> au = _Mapper.Map<List<DatosGenerale>>(request);
            au = _IDatosGeneraleRepository.InsertMultiple(au);
            List<DatosGeneraleResponse> res = _Mapper.Map<List<DatosGeneraleResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDatosGeneraleRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DatosGeneraleRequest> request)
        {
            List<DatosGenerale> au = _Mapper.Map<List<DatosGenerale>>(request);
            int cantidad = _IDatosGeneraleRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DatosGeneraleResponse> getAll()
        {
            List<DatosGenerale> lsl = _IDatosGeneraleRepository.GetAll();
            List<DatosGeneraleResponse> res = _Mapper.Map<List<DatosGeneraleResponse>>(lsl);
            return res;
        }

        public List<DatosGeneraleResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DatosGeneraleResponse getById(object id)
        {
            DatosGenerale au = _IDatosGeneraleRepository.GetById(id);
            DatosGeneraleResponse res = _Mapper.Map<DatosGeneraleResponse>(au);
            return res;
        }

        public DatosGeneraleResponse Update(DatosGeneraleRequest entity)
        {
            DatosGenerale au = _Mapper.Map<DatosGenerale>(entity);
            au = _IDatosGeneraleRepository.Update(au);
            DatosGeneraleResponse res = _Mapper.Map<DatosGeneraleResponse>(au);
            return res;
        }

        public List<DatosGeneraleResponse> UpdateMultiple(List<DatosGeneraleRequest> request)
        {
            List<DatosGenerale> au = _Mapper.Map<List<DatosGenerale>>(request);
            au = _IDatosGeneraleRepository.UpdateMultiple(au);
            List<DatosGeneraleResponse> res = _Mapper.Map<List<DatosGeneraleResponse>>(au);
            return res;
        }
    }
}
