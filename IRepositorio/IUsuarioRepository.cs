using DBModel.DB;
using Models.ResponseResponse;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface IUsuarioRepository: ICRUDRepositorio<Usuario>
    {
        Usuario GetByUserName(string userName);
        Task<List<string>> GetNotificationTokensAsync();

    }
}