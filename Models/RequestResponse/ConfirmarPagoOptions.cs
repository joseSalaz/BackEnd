namespace Models.RequestResponse
{
    /// <summary>
    /// Opciones para confirmar un pago online (MercadoPago / PayPal).
    /// Permite preservar las diferencias históricas entre proveedores sin duplicar lógica.
    /// </summary>
    public class ConfirmarPagoOptions
    {
        /// <summary>Estado inicial del pedido por cada detalle de venta.</summary>
        public string EstadoPedidoInicial { get; set; } = "Pedido Realizado";

        /// <summary>
        /// Id de caja a usar. Si es null, se usa la caja ecommerce configurada en el repositorio.
        /// </summary>
        public int? IdCaja { get; set; }

        /// <summary>
        /// Si true, incrementa SaldoFinal de la caja con el total del carrito.
        /// PayPal lo hace; MercadoPago históricamente no lo actualizaba.
        /// </summary>
        public bool ActualizarSaldoCaja { get; set; } = true;
    }
}
