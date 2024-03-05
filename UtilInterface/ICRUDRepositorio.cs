namespace UtilInterface
{
    public interface ICRUDRepositorio<T> : IDisposable where T : class 
    {
        /// <summary>
        /// Lista todfa la tabla
        /// </summary>
        /// <returns>resultado tabla</returns>
        List<T> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Valor del PK</param>
        /// <returns>retorna el registro de la tabla por ID</returns>
        T GetById(object id);
        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="entity">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        T Create(T entity);
        /// <summary>
        /// Actualiza un nuevo registro
        /// </summary>
        /// <param name="entity">Registro a Actualiza</param>
        /// <returns>retorna el registro Actualiza</returns>
        T Update(T entity);
        /// <summary>
        /// elimina un registro
        /// </summary>
        /// <param name="id">Valor del PK</param>
        /// <returns>Cantidad de registros afectados</returns>
        int Delete(object id);

        /// <summary>
        /// Elimina una lista de registros
        /// </summary>
        /// <param name="lista">lista de registros a eliminar</param>
        /// <returns>retorna la cantidad de registros a eliminar</returns>
        int DeleteMultipleItems(List<T> lista);
        /// <summary>
        /// inserta un lista de registrps
        /// </summary>
        /// <param name="lista">lista de registros a insertat</param>
        /// <returns>lista de registros inserttados</returns>
        List<T> InsertMultiple(List<T> lista);

        /// <summary>
        /// actualiza un lista de registrps
        /// </summary>
        /// <param name="lista">lista de registros a actualiza</param>
        /// <returns>lista de registros actualizados</returns>
        List<T> UpdateMultiple(List<T> lista);

        /// <summary>
        /// retora una lista de registros por coincidencia
        /// </summary>
        /// <param name="query">valor a buscar</param>
        /// <returns>lista de registros que coinciden</returns>
        List<T> GetAutoComplete(string query);
        //GenericFilterResponse<T> GetByFilter(GenericFilterRequest filter);
    }
}
