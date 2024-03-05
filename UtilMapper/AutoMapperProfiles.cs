using AutoMapper;
using DBModel.DB;
using Models.RequestRequest;
using Models.RequestResponse;
using Models.ResponseResponse;

namespace UtilMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
        
            CreateMap<Autor, AutorRequest>().ReverseMap();
            CreateMap<Autor, AutorResponse>().ReverseMap();

            CreateMap<Libro, LibroRequest>().ReverseMap();
            CreateMap<Libro, LibroResponse>().ReverseMap();

            CreateMap<Usuario, UsuarioRequest>().ReverseMap();  
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();

            CreateMap<Categoria, CategoriaRequest>().ReverseMap();
            CreateMap<Categoria, CategoriaResponse>().ReverseMap();


            CreateMap<Cliente, ClienteRequest>().ReverseMap();
            CreateMap<Cliente, ClienteResponse>().ReverseMap();

            CreateMap<LibroGenero, LibroGeneroRequest>().ReverseMap();
            CreateMap<LibroGenero, LibroGeneroResponse>().ReverseMap();

            CreateMap<Usuario, UsuarioRequest>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();

            CreateMap<Persona, PersonaRequest>().ReverseMap();
            CreateMap<Persona, PersonaResponse>().ReverseMap();



            CreateMap<Genero, GeneroRequest>().ReverseMap();
            CreateMap<Genero, GeneroResponse>().ReverseMap();

            CreateMap<LibroAutor, LibroAutorRequest>().ReverseMap();
            CreateMap<LibroAutor, LibroAutorResponse>().ReverseMap();



            CreateMap<Venta, VentaRequest>().ReverseMap();
            CreateMap<Venta, VentaResponse>().ReverseMap();


        }

    }
}
