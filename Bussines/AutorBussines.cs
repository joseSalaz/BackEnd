using AutoMapper;
using Azure.Core;
using DBModel.DB;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using IBussines;
using IRepositorio;
using IRepository;
using Models.RequestResponse;
using Repository;
<<<<<<< HEAD
using UnitOfWork;
=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4

namespace Bussines
{
  public class AutorBussines : IAutorBussines

  {
    #region Declaracion de vcariables generales
    public readonly IAutorRepository _IAutorRepository = null;
    public readonly IMapper _Mapper;

    #endregion

<<<<<<< HEAD
    private readonly IUnitOfWork _unitOfWork;

    #region constructor 
    public AutorBussines(IMapper mapper, IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      _Mapper = mapper;
      _IAutorRepository = _unitOfWork.Autores;
=======
    #region constructor 
    public AutorBussines(IMapper mapper)
    {
      _Mapper = mapper;
      _IAutorRepository = new AutorRepository();
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
    }
    #endregion

    public AutorResponse Create(AutorRequest entity)
    {
      Autor au = _Mapper.Map<Autor>(entity);
      au = _IAutorRepository.Create(au);
      AutorResponse res = _Mapper.Map<AutorResponse>(au);
      return res;
    }

    public List<AutorResponse> CreateMultiple(List<AutorRequest> request)
    {
      List<Autor> au = _Mapper.Map<List<Autor>>(request);
      au = _IAutorRepository.InsertMultiple(au);
      List<AutorResponse> res = _Mapper.Map<List<AutorResponse>>(au);
      return res;
    }

    public int Delete(object id)
    {
      return _IAutorRepository.Delete(id);
    }

    public int deleteMultipleItems(List<AutorRequest> request)
    {
      List<Autor> au = _Mapper.Map<List<Autor>>(request);
      int cantidad = _IAutorRepository.DeleteMultipleItems(au);
      return cantidad;
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }

    public List<AutorResponse> getAll()
    {
      List<Autor> lsl = _IAutorRepository.GetAll();
      List<AutorResponse> res = _Mapper.Map<List<AutorResponse>>(lsl);
      return res;
    }

    public List<AutorResponse> getAutoComplete(string query)
    {
      throw new NotImplementedException();
    }

    public AutorResponse getById(object id)
    {
      Autor au = _IAutorRepository.GetById(id);
      AutorResponse res = _Mapper.Map<AutorResponse>(au);
      return res;
    }

    public AutorResponse Update(AutorRequest entity)
    {
      Autor au = _Mapper.Map<Autor>(entity);
      au = _IAutorRepository.Update(au);
      AutorResponse res = _Mapper.Map<AutorResponse>(au);
      return res;
    }

    public List<AutorResponse> UpdateMultiple(List<AutorRequest> request)
    {
      List<Autor> au = _Mapper.Map<List<Autor>>(request);
      au = _IAutorRepository.UpdateMultiple(au);
      List<AutorResponse> res = _Mapper.Map<List<AutorResponse>>(au);
      return res;
    }

    public async Task<Autor> GetByNameAsync(string nombre)
    {
      return await _IAutorRepository.GetByName(nombre);
    }

    public async Task<List<AutorCategoria>> GetAutorCategoria(int idCategoria, int? idSubcategoria)
    {
      List<Autor> ca = await _IAutorRepository.GetAutorCategoria(idCategoria, idSubcategoria);
      return ca.Select(a => new AutorCategoria
      {
        IdAutor = a.IdAutor,
        Nombre = a.Nombre,
        Apellido = a.Apellido
      }).ToList();
    }
  }
}
