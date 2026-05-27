using AutoMapper;
using DBModel.DB;
using IBussnies;
using IRepository;
using Models.RequestRequest;
using Models.RequestResponse;
using Models.ResponseResponse;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussnies
{
    public class UsuarioBussnies : IUsuarioBussnies
    {
        #region declaracion variables generales
        public readonly IUsuarioRepository _IUsuarioRepository = null;
        public readonly IMapper _mapper;
        #endregion

        #region constructor
        public UsuarioBussnies(IMapper mapper)
        {
            _mapper = mapper;
            _IUsuarioRepository = new UsuarioRepository();
        }
        #endregion


        /* inyección de dependencias */

        public UsuarioResponse Create(UsuarioRequest entity)
        {
            Usuario cat = _mapper.Map<Usuario>(entity);
            cat = _IUsuarioRepository.Create(cat);
            UsuarioResponse res = _mapper.Map<UsuarioResponse>(cat);
            return res;
        }

        public List<UsuarioResponse> CreateMultiple(List<UsuarioRequest> request)
        {
            List<Usuario> cat = _mapper.Map<List<Usuario>>(request);
            cat = _IUsuarioRepository.InsertMultiple(cat);
            List<UsuarioResponse> res = _mapper.Map<List<UsuarioResponse>>(cat);
            return res;
        }

        public int Delete(object id)
        {
            return _IUsuarioRepository.Delete(id);
        }

        public int deleteMultipleItems(List<UsuarioRequest> request)
        {
            List<Usuario> cat = _mapper.Map<List<Usuario>>(request);
            int cantidad = _IUsuarioRepository.DeleteMultipleItems(cat);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<UsuarioResponse> getAll()
        {
            List<Usuario> lst = _IUsuarioRepository.GetAll();
            List<UsuarioResponse> res = _mapper.Map<List<UsuarioResponse>>(lst);
            return res;
        }

        public List<UsuarioResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public UsuarioResponse getById(object id)
        {
            Usuario cat = _IUsuarioRepository.GetById(id);
            UsuarioResponse res = _mapper.Map<UsuarioResponse>(cat);
            return res;
        }

        public UsuarioResponse GetByUserName(string userName)
        {
            Usuario user = _IUsuarioRepository.GetByUserName(userName);
            UsuarioResponse res = _mapper.Map<UsuarioResponse>(user);
            return res;
        }

        public UsuarioResponse Update(UsuarioRequest entity)
        {
            Usuario cat = _mapper.Map<Usuario>(entity);
            cat = _IUsuarioRepository.Update(cat);
            UsuarioResponse res = _mapper.Map<UsuarioResponse>(cat);
            return res;
        }

        public List<UsuarioResponse> UpdateMultiple(List<UsuarioRequest> request)
        {
            List<Usuario> cat = _mapper.Map<List<Usuario>>(request);
            cat = _IUsuarioRepository.UpdateMultiple(cat);
            List<UsuarioResponse> res = _mapper.Map<List<UsuarioResponse>>(cat);
            return res;
        }

        public bool RegisterNotificationToken(int usuarioId, string token)
        {
            var usuario = _IUsuarioRepository.GetById(usuarioId); if (usuario == null)
            {
                return false;
            }
            usuario.NotificationToken = token; _IUsuarioRepository.Update(usuario); // Actualiza el usuario a través del repositorio
            return true;
        }
        public async Task<List<string>> GetNotificationTokensAsync()
        { 
            return await _IUsuarioRepository.GetNotificationTokensAsync(); 
        }

        public async Task<bool> CrearUsuarioAsync(UsuarioRequest request)
        {
            return await _IUsuarioRepository.CrearUsuarioAsync(request);
        }

        public async Task<bool> ActualizarUsuarioAsync(UsuarioRequest request)
        {
            return await _IUsuarioRepository.ActualizarUsuarioAsync(request);
        }

        public async Task<bool> CambiarEstadoUsuario(int usuarioId, bool estadoActual)
        {
            return await _IUsuarioRepository.CambiarEstadoUsuario(usuarioId, estadoActual);
        }

    }
}