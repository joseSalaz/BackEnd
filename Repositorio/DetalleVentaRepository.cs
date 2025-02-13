using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.RequestResponse;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DetalleVentaRepository : GenericRepository<DetalleVenta>, IDetalleVentaRepository
    {


        public List<DetalleVenta> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentasByPersonaId(int idPersona)
        {
            var detalleVentas = await db.DetalleVentas
                                        .Include(dv => dv.IdVentasNavigation) 
                                        .Where(dv => dv.IdVentasNavigation.IdPersona == idPersona)
                                        .ToListAsync();
            return detalleVentas;
        }

        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentasByVentaId(int idVenta)
        {
            var detalleVentas = await db.DetalleVentas
                                        .Include(dv => dv.IdVentasNavigation) // Relación con la tabla Venta
                                        .Where(dv => dv.IdVentas == idVenta)   // Filtra por el id de la Venta
                                        .ToListAsync();
            return detalleVentas;
        }


        public async Task<bool> UpdateEstadoPedidosByVentaId(int idVenta, EstadoPedidoRequest request)
        {
            // Obtener los ids de detalle venta asociados al idVenta
            var detalleVentas = await db.DetalleVentas
                                        .Where(dv => dv.IdVentas == idVenta)
                                        .Select(dv => dv.IdDetalleVentas)
                                        .ToListAsync();

            // Si no hay detalles de venta, no hacemos nada
            if (!detalleVentas.Any())
                return false;

            // Filtrar los registros de EstadoPedido relacionados con los detalles de venta
            var estadosPedidos = await db.EstadoPedidos
                                        .Where(ep => detalleVentas.Contains(ep.IdDetalleVentas))
                                        .ToListAsync();

            // Actualizar los campos para cada registro de EstadoPedido
            foreach (var estadoPedido in estadosPedidos)
            {
                estadoPedido.Estado = request.Estado;
                estadoPedido.FechaEstado = request.FechaEstado;
                estadoPedido.Comentario = request.Comentario;
            }

            // Guardar los cambios
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductosMasVendidosResponse>> ObtenerProductosMasVendidosAsync(int mes, int anio)
        {
            var query = @"
WITH VentasMes AS (
    SELECT 
        dv.idLibro, 
        l.Titulo AS nombreProducto, 
        dv.Cantidad
    FROM Ventas v
    INNER JOIN Detalle_Ventas dv ON v.id_Ventas = dv.id_Ventas
    INNER JOIN Libro l ON dv.idLibro = l.idLibro
    WHERE MONTH(v.Fecha_Venta) = @Mes
      AND YEAR(v.Fecha_Venta) = @Anio
)
SELECT 
    idLibro, 
    nombreProducto, 
    SUM(Cantidad) AS TotalVendidos
FROM VentasMes
GROUP BY idLibro, nombreProducto
ORDER BY TotalVendidos DESC";

            var productosMasVendidos = new List<ProductosMasVendidosResponse>();

            using (var connection = db.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    // Parámetros
                    var paramMes = command.CreateParameter();
                    paramMes.ParameterName = "@Mes";
                    paramMes.Value = mes;
                    command.Parameters.Add(paramMes);

                    var paramAnio = command.CreateParameter();
                    paramAnio.ParameterName = "@Anio";
                    paramAnio.Value = anio;
                    command.Parameters.Add(paramAnio);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productosMasVendidos.Add(new ProductosMasVendidosResponse
                            {
                                ProductoId = reader.GetInt32(0),
                                NombreProducto = reader.GetString(1),
                                TotalVendidos = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }

            return productosMasVendidos;
        }





    }
}
