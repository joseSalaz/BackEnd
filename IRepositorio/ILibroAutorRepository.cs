using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface ILibroAutorRepository : ICRUDRepositorio<LibroAutor>
    {
        public Task<List<Autor>> GetAutoresByLibroId(int libroId);
        public Task<List<Libro>> GetLibrosByAutorId(int autorId);
    }
}
