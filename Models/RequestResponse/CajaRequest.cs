using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class CajaRequest
    {
        public int IdCaja { get; set; }

        public decimal? SaldoInicial { get; set; }

        public decimal? SaldoFinal { get; set; }

        public DateTime? Fecha { get; set; }

<<<<<<< HEAD

=======
>>>>>>> 659483f76575f0aaa47ab234b486fc7fa8bd8bea
        public decimal? RetiroDeCaja { get; set; }

        public decimal? IngresosACaja { get; set; }
    }
}
