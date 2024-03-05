using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class GenericResponse
    {

        public bool Success { get; set; } = false;
        public string Codigo { get; set; } = "ERR";
        public string Descripcion { get; set; } = "OCURRIO UN ERROR INTERNO";
        public string Mensaje { get; set; } = "Revise el codigo de error";
        public string MensajeSistema { get; set; } = "OCURRIO UN ERROR COMUNIQUESE CON EL AREA DE SISTEMAS";
            
    }
}
