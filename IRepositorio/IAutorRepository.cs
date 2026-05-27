using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface  IAutorRepository: ICRUDRepositorio<Autor>
    {
        Task<Autor> GetByIds(List<int> ids);
        Task<Autor> GetByName(string nombre);
        Task<Autor> GetByIdAsync(object id);
    }
}
