using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilInterface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Request">Clase de request</typeparam>
    /// <typeparam name="Response">Clase de response</typeparam>
    public interface ICRUDBussnies<Request, Response> : IDisposable
    {
        /// <summary>
        /// Listar todo
        /// </summary>
        /// <returns>Lista de una clase desde de la base de datos</returns>
        List<Response> getAll();

        /// <summary>
        /// Retorna registro en base al primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Response getById(object id);
        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity">clase a insertar</param>
        /// <returns>mismo registro</returns>
        Response Create(Request entity);
        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">clase a actualizar</param>
        /// <returns>el mismo registro</returns>
        Response Update(Request entity);
        /// <summary>
        /// Elimina un registro en base al OPK
        /// </summary>
        /// <param name="id">valor de PK</param>
        /// <returns>cantidad de registros afectadis</returns>
        int Delete(object id);

        /// <summary>
        /// elimina multiples registro 
        /// </summary>
        /// <param name="request">Lista de registros a eliminar</param>
        /// <returns></returns>
        int deleteMultipleItems(List<Request> request);

        /// <summary>
        /// Inserción de multiplos registros en tabla
        /// </summary>
        /// <param name="request">lista de registros a insertar</param>
        /// <returns>Los registros insertados</returns>
        List<Response> CreateMultiple(List<Request> request);
        /// <summary>
        /// actualización de multiples registros en tabla
        /// </summary>
        /// <param name="request">lista de registros a insertar</param>
        /// <returns>Los registros insertados</returns>
        List<Response> UpdateMultiple(List<Request> request);
        //List<Y> getByAutoComplete(string query);
        /// <summary>
        /// obtener lista de registros por coincidencia
        /// </summary>
        /// <param name="query">texto a buscar</param>
        /// <returns>lista de registros</returns>
        List<Response> getAutoComplete(string query);
    }
}
