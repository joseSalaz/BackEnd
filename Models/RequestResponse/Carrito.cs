using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class Carrito
    {
        public LibroRequest libro {  get; set; }
        public decimal PrecioVenta {  get; set; }
        public int Cantidad {  get; set; }
    }
}
