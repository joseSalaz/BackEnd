using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Caja
{
    public int IdCaja { get; set; }

    public decimal? SaldoInicial { get; set; }

    public decimal? SaldoFinal { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? RetiroDeCaja { get; set; }

    public decimal? IngresosACaja { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
