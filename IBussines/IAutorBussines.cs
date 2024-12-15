using Azure.Core;
using DBModel.DB;
using Models.RequestResponse;
using UtilInterface;

namespace IBussines
{
    public interface IAutorBussines: ICRUDBussnies<AutorRequest, AutorResponse>
    {
    }
}