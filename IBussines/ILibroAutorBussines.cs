using DBModel.DB;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface ILibroAutorBussines : ICRUDBussnies<LibroAutorRequest,LibroAutorResponse>
    {
        Task<List<Libro>> GetLibrosByAutorId(int autorId);
        Task<List<Autor>> GetAutoresByLibroId(int libroId);
    }
}
