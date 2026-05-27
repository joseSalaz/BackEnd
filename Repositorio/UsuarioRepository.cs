using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Models.RequestRequest;
using Models.ResponseResponse;
using Repository.Generic;
using UtilSecurity.UtilSecurity;

namespace Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {

        public List<Usuario> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public Usuario GetByUserName(string userName)
        {
            Usuario user = dbSet.Where(x => x.Username == userName).FirstOrDefault();
            return user;
        }

        public async Task<List<string>> GetNotificationTokensAsync() 
        { 
            return await dbSet.Where(u => !string.IsNullOrEmpty(u.NotificationToken)).Select(u => u.NotificationToken).ToListAsync(); 
        }

        public async Task<bool> CrearUsuarioAsync(UsuarioRequest request)
        {
            // Verificar que la persona exista antes de crear el usuario
            var personaExiste = await db.Personas
                                       .AnyAsync(p => p.IdPersona == request.IdPersona);

            if (!personaExiste)
            {
                return false; // La persona no existe, retornamos false
            }

            // Encriptar la contraseña antes de guardarla
            string passwordEncriptada = UtilCripto.encriptar_AES(request.Password);

            // Crear el usuario con la referencia a la persona
            var usuario = new Usuario
            {
                Username = request.Username,
                Password = passwordEncriptada,
                Cargo = request.Cargo,
                Estado = request.Estado,
                IdPersona = request.IdPersona
            };

            // Agregar el usuario a la base de datos
            await db.Usuarios.AddAsync(usuario);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActualizarUsuarioAsync(UsuarioRequest request)
        {
            var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == request.IdUsuario);
            if (usuario == null) return false;

            usuario.Username = request.Username;
            usuario.Cargo = request.Cargo;
            usuario.Estado = request.Estado;
            usuario.IdPersona = request.IdPersona;

            if (!string.IsNullOrEmpty(request.Password))
            {
                usuario.Password = UtilCripto.encriptar_AES(request.Password);
            }

            await db.SaveChangesAsync();
            return true;
        }


        public async Task<bool> CambiarEstadoUsuario(int usuarioId, bool estadoActual)
        {
            var usuario = await dbSet.FindAsync(usuarioId);
            if (usuario == null)
            {
                return false; // Retornar false si el usuario no existe
            }

            usuario.Estado = !estadoActual; // Alternar estado según el valor recibido
            await db.SaveChangesAsync();

            return true; // Retornar true si se actualizó correctamente
        }


    }
}