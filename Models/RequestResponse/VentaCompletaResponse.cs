namespace Models.RequestResponse
{
    /// <summary>
    /// Resultado de registrar una venta completa (persona + venta + caja + kardex + detalle)
    /// como una única operación transaccional.
    /// </summary>
    public class VentaCompletaResponse
    {
        public string Mensaje { get; set; } = string.Empty;
        public VentaResponse Venta { get; set; } = null!;
        public List<DetalleVentaResponse> Detalles { get; set; } = new();
    }
}
