using DBModel.DB;
using Models.ResponseResponse;
using UtilInterface;

namespace IRepository
{
    public interface IUsuarioRepository: ICRUDRepositorio<Usuario>
    {
        Usuario GetByUserName(string userName);
    }
}