using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LibroResponse
    {
        public int IdLibro { get; set; }

        public string? Titulo { get; set; }

        public string? Isbn { get; set; }

        public string? Tamanno { get; set; }

        public string? Descripcion { get; set; }

        public string? Condicion { get; set; }

        public string? Impresion { get; set; }

        public string? TipoTapa { get; set; }

        public bool? Estado { get; set; }

        public int IdSubcategoria { get; set; }

        public int IdTipoPapel { get; set; }

        public string TipoPapelDescripcion
        {
            get
            {
                return ObtenerDescripcionTipoPapel(IdTipoPapel);
            }
        }

        private string ObtenerDescripcionTipoPapel(int idTipoPapel)
        {
            // Aquí deberías tener la lógica para mapear 'idTipoPapel' a su descripción.
            // Esto puede ser una búsqueda en una base de datos, un diccionario en memoria, etc.
            switch (idTipoPapel)
            {
                case 1:
                    return "Papel ahuesado";
                case 2:
                    return "Papel bond";
                case 3:
                    return "Cartón";
                case 4:
                    return "Papel couché";
                case 5:
                    return "Papel periódico";
                default:
                    return "Desconocido";
            }
        }
        public int IdProveedor { get; set; }

        public string? Imagen { get; set; }
        public string EstadoDescripcion
        {
            get
            {
                if (Estado.HasValue)
                {
                    return Estado.Value ? "Disponible" : "No Disponible";
                }
                else
                {
                    // Si Estado es nulo, devolver un valor predeterminado, por ejemplo, "Desconocido"
                    return "Desconocido";
                }
            }
        }

        public static class CategoriaHelper
        {
            private static readonly Dictionary<int, string> _categorias = new Dictionary<int, string>
            {
                { 1, "Agendas" },
                { 2, "Inicial" },
                { 3, "Primaria" },
                { 4, "Secundaria" },
                { 5, "Literatura Infantil" },
                { 6, "Tarjetas Lexicas" },
                { 7, "Literatura" },
                { 8, "Diccionarios" },
                { 9, "Superior" },
                { 10, "Bizarro" },
                { 11, "Miscelanea" },
                { 12, "PreUniversitarrio" },
                // Añade todas las categorias que necesitas
             };
        }
    }
}
