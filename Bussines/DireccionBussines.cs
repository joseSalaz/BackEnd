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
    public class DireccionBussines : IDireccionBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDireccionRepository _IDireccionRepository = null;
        public readonly IMapper _Mapper;


        #endregion

<<<<<<< HEAD
        private readonly IUnitOfWork _unitOfWork;

    #region constructor 
        public DireccionBussines(IMapper mapper, IUnitOfWork unitOfWork)
        {
      _unitOfWork = unitOfWork;
            _Mapper = mapper;
            _IDireccionRepository = _unitOfWork.Direcciones;
=======
        #region constructor 
        public DireccionBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDireccionRepository = new DireccionRepository();
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        }
        #endregion

        public DireccionResponse Create(DireccionRequest entity)
        {
            Direccion au = _Mapper.Map<Direccion>(entity);
            au = _IDireccionRepository.Create(au);
            DireccionResponse res = _Mapper.Map<DireccionResponse>(au);
            return res;
        }

        public List<DireccionResponse> CreateMultiple(List<DireccionRequest> request)
        {
            List<Direccion> au = _Mapper.Map<List<Direccion>>(request);
            au = _IDireccionRepository.InsertMultiple(au);
            List<DireccionResponse> res = _Mapper.Map<List<DireccionResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDireccionRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DireccionRequest> request)
        {
            List<Direccion> au = _Mapper.Map<List<Direccion>>(request);
            int cantidad = _IDireccionRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DireccionResponse> getAll()
        {
            List<Direccion> lsl = _IDireccionRepository.GetAll();
            List<DireccionResponse> res = _Mapper.Map<List<DireccionResponse>>(lsl);
            return res;
        }

        public List<DireccionResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DireccionResponse getById(object id)
        {
            Direccion au = _IDireccionRepository.GetById(id);
            DireccionResponse res = _Mapper.Map<DireccionResponse>(au);
            return res;
        }

        public DireccionResponse Update(DireccionRequest entity)
        {
            Direccion au = _Mapper.Map<Direccion>(entity);
            au = _IDireccionRepository.Update(au);
            DireccionResponse res = _Mapper.Map<DireccionResponse>(au);
            return res;
        }

        public List<DireccionResponse> UpdateMultiple(List<DireccionRequest> request)
        {
            List<Direccion> au = _Mapper.Map<List<Direccion>>(request);
            au = _IDireccionRepository.UpdateMultiple(au);
            List<DireccionResponse> res = _Mapper.Map<List<DireccionResponse>>(au);
            return res;
        }
        public async Task<List<DireccionResponse>> GetDireccionesPorUsuario(int idUsuario)
        {
            var direcciones = await _IDireccionRepository.GetDireccionesPorUsuario(idUsuario);
            return _Mapper.Map<List<DireccionResponse>>(direcciones);
        }

        public async Task<bool> SetDireccionPredeterminada(int idDireccion)
        {
            var direccion = _IDireccionRepository.GetById(idDireccion);
            if (direccion == null) return false;

            // Desmarcar otras direcciones predeterminadas del usuario
            var direccionesUsuario = await _IDireccionRepository.GetDireccionesPorUsuario(direccion.IdPersona);
            foreach (var dir in direccionesUsuario)
            {
                dir.EsPredeterminada = false;
                _IDireccionRepository.Update(dir);
            }

            // Marcar la nueva dirección como predeterminada
            direccion.EsPredeterminada = true;
            _IDireccionRepository.Update(direccion);

            return true;
        }


    }
}
