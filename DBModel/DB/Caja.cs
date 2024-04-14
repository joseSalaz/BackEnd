namespace DBModel.DB;

public partial class Caja
{
    public int IdCaja { get; set; }

    public decimal? SaldoInicial { get; set; }

    public decimal? SaldoFinal { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdVentas { get; set; }

    public decimal? RetiroDeCaja { get; set; }

    public decimal? IngresosACaja { get; set; }

    public virtual Venta IdVentasNavigation { get; set; } = null!;
}
