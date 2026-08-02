using Constantes;
using DBModel.DB;
using Microsoft.EntityFrameworkCore;
using Models.Comon;
using System.Linq.Expressions;
using System.Net;


namespace Repository.Generic
{
  /// <summary>
  /// Repositorio genérico basado en Entity Framework Core.
  /// IMPORTANTE: ya NO crea su propio DbContext. El contexto se inyecta por constructor
  /// para que pueda ser compartido por UnitOfWork entre varios repositorios y así permitir
  /// transacciones reales (commit/rollback) entre operaciones de distintas entidades.
  /// </summary>

  public class GenericRepository<TEntity> where TEntity : class
  {
    internal LibreriaSaberContext db = new LibreriaSaberContext();
    internal DbSet<TEntity> dbSet;
    /// <summary>
    /// El contexto SIEMPRE se recibe por fuera (DI o UnitOfWork). Nunca se crea aquí,
    /// para que todos los repositorios de una misma unidad de trabajo compartan la misma
    /// conexión/transacción.
    /// </summary>
    public GenericRepository(LibreriaSaberContext context)
    {
      db = context ?? throw new ArgumentNullException(nameof(context), "El DbContext debe ser inyectado, no se crea internamente.");
      dbSet = db.Set<TEntity>();
    }
    public GenericRepository()
    {
      try
      {

        this.dbSet = db.Set<TEntity>();

      }
      catch (Exception ex)
      {


        throw ex;

      }
    }


    public virtual IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] navigationProperties)
    {
      IQueryable<TEntity> query = dbSet;
      foreach (var navigationProperty in navigationProperties)
      {
        query = query.Include(navigationProperty);
      }
      return query;
    }

    /// <summary>
    /// No libera el DbContext: su ciclo de vida lo controla quien lo inyectó (DI / UnitOfWork).
    /// </summary>

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }

    public virtual List<TEntity> GetAll()
    {
      try
      {
        IQueryable<TEntity> query = dbSet;
        return query.ToList();

      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Ocurrio un error al obtener toda la lista", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
    }


    public virtual TEntity GetById(object id)
    {
      try
      {

        return dbSet.Find(id);

      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Ocurrio un error al buscar el registro", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
    }

    public virtual TEntity Create(TEntity entity)
    {
      try
      {
        dbSet.Add(entity);
        db.SaveChanges();
        return entity;
      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Error al registrar", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
    }

    public virtual TEntity Update(TEntity entity)
    {
      try
      {
        dbSet.Update(entity);
        db.SaveChanges();
        return entity;

      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Error al actualizar", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
    }


    public virtual int Delete(object id)
    {
      try
      {

        TEntity entityToDelete = dbSet.Find(id);


        dbSet.Remove(entityToDelete);

        return db.SaveChanges();
      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Error al eliminar", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }

    }

    public virtual int DeleteMultipleItems(List<TEntity> lista)
    {
      try
      {
        dbSet.RemoveRange(lista);
        return db.SaveChanges();
      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Error al eliminar multiple", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }

    }
    public virtual List<TEntity> InsertMultiple(List<TEntity> lista)
    {
      try
      {
        dbSet.AddRange(lista);
        db.SaveChanges();
        return lista;
      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Error al eliminar multiple", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
    }
    public virtual List<TEntity> UpdateMultiple(List<TEntity> lista)
    {
      try
      {
        dbSet.UpdateRange(lista);
        db.SaveChanges();
        return lista;
      }
      catch (DbUpdateException ex)
      {
        CustomException exx = new CustomException("Error en base de datos", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
      catch (Exception ex)
      {
        CustomException exx = new CustomException("Error al eliminar multiple", (int)HttpStatusCode.InternalServerError, ConstantesResultado.ERROR_NO_CONTROLADO_CODIGO, "No Controlado", ex);
        throw exx;
      }
    }
  }
}
