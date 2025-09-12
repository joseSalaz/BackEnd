using DBModel.DB;
using Models.RequestRequest;
using Models.ResponseResponse;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface IUsuarioRepository: ICRUDRepositorio<Usuario>
    {
        Usuario GetByUserName(string userName);
        Task<List<string>> GetNotificationTokensAsync();
        Task<bool> CrearUsuarioAsync(UsuarioRequest request);
        Task<bool> ActualizarUsuarioAsync(UsuarioRequest request);
        Task<bool> CambiarEstadoUsuario(int usuarioId, bool estadoActual);

    }
}