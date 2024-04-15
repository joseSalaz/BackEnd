using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class CajaResponse
    {
        public int IdCaja { get; set; }

        public decimal? SaldoInicial { get; set; }

        public decimal? SaldoFinal { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? RetiroDeCaja { get; set; }

        public decimal? IngresosACaja { get; set; }
    }
}
