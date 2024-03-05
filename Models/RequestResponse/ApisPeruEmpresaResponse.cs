using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class ApisPeruEmpresaResponse
    {
        public string ruc { get; set; } = "";
        public string razonSocial { get; set; } = "";
        public string nombreComercial { get; set; } = "";
        public string tipo { get; set; } = "";
        public string estado { get; set; } = "";
        public string condicion { get; set; } = "";
        public string direccion { get; set; } = "";
        public string departamento { get; set; } = "";
        public string provincia { get; set; } = "";
        public string distrito { get; set; } = "";
        public string ubigeo { get; set; } = "";
        public string capital { get; set; } = "";
        public string fechaInscripcion { get; set; } = "";
        public string sistEmsion { get; set; } = "";
        public string sistContabilidad { get; set; } = "";
        public string actExterior { get; set; } = "";
        public string fechaEmisorFe { get; set; } = "";
        public string fechaPle { get; set; } = "";
        public string fechaBaja { get; set; } = "";
        public string profesion { get; set; } = "";
    }
}
