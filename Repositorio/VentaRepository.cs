using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {

        public List<Venta> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DetalleVenta>> GetDetallesByVentaId(int idVenta)
        {
            var ventaConDetalles = await dbSet
                .Include(v => v.DetalleVenta) // Usar el nombre correcto de la propiedad
                .FirstOrDefaultAsync(v => v.IdVentas == idVenta); // Asegúrate de usar el nombre correcto del ID

            return ventaConDetalles?.DetalleVenta.ToList() ?? new List<DetalleVenta>();
        }

        public async Task<(Venta venta, List<DetalleVenta> detalles)> GetVentaConDetalles(int idVenta)
        {
            var ventaConDetalles = await dbSet
                .Include(v => v.DetalleVenta) // Cargar los detalles de venta
                .FirstOrDefaultAsync(v => v.IdVentas == idVenta);

            // Si no se encuentra la venta, devolver una venta nula y una lista vacía de detalles.
            if (ventaConDetalles == null) return (null, new List<DetalleVenta>());

            // Devolver tanto la venta como los detalles de venta.
            return (ventaConDetalles, ventaConDetalles.DetalleVenta.ToList());
        }

        public async Task<Persona> GetPersonaByVentaId(int idVenta)
        {
            // Intenta obtener la venta incluyendo los detalles de la persona asociada usando la propiedad de navegación.
            var venta = await dbSet
                .Include(v => v.IdPersonaNavigation)  // Usar la propiedad de navegación para incluir la entidad Persona.
                .FirstOrDefaultAsync(v => v.IdVentas == idVenta);

            // Devuelve la persona asociada, o null si no se encuentra la venta.
            return venta?.IdPersonaNavigation;
        }

        public async Task<string> ObtenerUltimoNumeroComprobante()
        {
            var ultimoComprobante = await db.Ventas
                        .OrderByDescending(v => v.FechaVenta)
                        .Select(v => v.NroComprobante)
                        .FirstOrDefaultAsync();

            return ultimoComprobante;
        }


        public async Task<IEnumerable<Venta>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await dbSet
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta < fechaFin.AddDays(1))
            .ToListAsync();

        }
        public async Task<(List<Venta>, int)> GetVentaPaginados(int page, int pageSize, string estado = null, bool ordenarPorFechaDesc = true, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            var query = dbSet
                .Include(v => v.DetalleVenta)
                .ThenInclude(dv => dv.EstadoPedidos)
                .AsQueryable();

            // Filtrar por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(v => v.DetalleVenta.Any(dv => dv.EstadoPedidos.Any(ep => ep.Estado == estado)));
            }

            // Filtrar fechas
            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                var inicio = fechaInicio.Value.Date; // Normaliza para solo usar la fecha
                var fin = fechaFin.Value.Date.AddDays(1).AddTicks(-1); // Incluye todo el día final

                query = query.Where(v => v.FechaVenta >= inicio && v.FechaVenta <= fin);
            }


            // Orden fechas
            query = ordenarPorFechaDesc
                ? query.OrderByDescending(v => v.FechaVenta)
                : query.OrderBy(v => v.FechaVenta);

            // Contar registro
            int totalItems = await query.CountAsync();

            // Paginado
            var ventas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (ventas, totalItems);
        }

        public async Task<(Venta venta, List<DetalleVenta> detalles, EstadoPedido estado)> GetVentaConDetallesYEstado(int idVenta)
        {
            var venta = await dbSet
                .Include(v => v.DetalleVenta)
                    .ThenInclude(dv => dv.EstadoPedidos) 
                .FirstOrDefaultAsync(v => v.IdVentas == idVenta);

            if (venta == null)
            {
                return (null, new List<DetalleVenta>(), null);
            }

            var detalles = venta.DetalleVenta.ToList();
            var estadoPedido = detalles
                .SelectMany(dv => dv.EstadoPedidos)
                .OrderByDescending(ep => ep.FechaEstado)
                .FirstOrDefault();

            return (venta, detalles, estadoPedido);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await db.SaveChangesAsync();
        }
        public bool ExisteVentaConDireccion(int idDireccion)
        {
            return db.Ventas.Any(v => v.IdDireccion == idDireccion);
        }
        public void AsignarDireccionAVenta(Venta venta, int idDireccion)
        {
            venta.IdDireccion = idDireccion;
            dbSet.Update(venta);
        }


        public async Task<List<Venta>> ObtenerVentasPorIdPersona(int idPersona)
        {
            return await db.Ventas
                .Where(v => v.IdPersona == idPersona)
                .Include(v => v.DetalleVenta) // Relacionar detalles de venta
                .ToListAsync();
        }

        // Obtener los detalles de venta por ID de venta
        public async Task<List<DetalleVenta>> ObtenerDetallesPorIdVenta(int idVenta)
        {
            return await db.DetalleVentas
                .Where(dv => dv.IdVentas == idVenta)
                .ToListAsync();
        }

        // Obtener el estado del pedido por ID de detalle de venta (solo uno)
        public async Task<EstadoPedido> ObtenerEstadoPedidoUnicoPorVenta(int idDetalleVenta)
        {
            return await db.EstadoPedidos
                .Where(ep => ep.IdDetalleVentas == idDetalleVenta)
                .Include(ep => ep.EstadoPedidoImagenes)
                .FirstOrDefaultAsync();
        }

        public async Task<VentaDetalledireccionResponse> GetVentaConPersonaYDireccion(int idVenta)
        {
            const string query = @"
SELECT 
    v.Id_Ventas, v.Total_Precio, v.Tipo_Comprobante, v.Fecha_Venta, v.NroComprobante,
    p.Id_Persona, p.Nombre, p.ApellidoPaterno, p.ApellidoMaterno, p.Correo,p.Telefono,p.Numero_Documento,p.Tipo_Documento,
    d.Id_Direccion, d.Direccion, d.Referencia, d.Departamento, d.Provincia, d.Distrito, d.CodigoPostal
FROM Detalle_Ventas dv
JOIN ventas v ON dv.id_Ventas = v.Id_Ventas
JOIN persona p ON v.Id_Persona = p.Id_Persona
LEFT JOIN direccion d ON v.Id_Direccion = d.Id_Direccion
WHERE dv.id_Ventas = @idVenta";

            using var connection = db.Database.GetDbConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = query;

            var paramIdVenta = command.CreateParameter();
            paramIdVenta.ParameterName = "@idVenta";
            paramIdVenta.Value = idVenta;
            command.Parameters.Add(paramIdVenta);

            using var reader = await command.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            return new VentaDetalledireccionResponse
            {
                Id_Ventas = reader.GetInt32(0),
                Total_Precio = reader.GetDecimal(1),
                Tipo_Comprobante = reader.GetString(2),
                Fecha_Venta = reader.GetDateTime(3),
                NroComprobante = reader.IsDBNull(4) ? "" : reader.GetString(4),  // Manejo de NULL

                Id_Persona = reader.GetInt32(5),
                Nombre = reader.IsDBNull(6) ? "" : reader.GetString(6),  // Manejo de NULL
                ApellidoPaterno = reader.IsDBNull(7) ? "" : reader.GetString(7),
                ApellidoMaterno = reader.IsDBNull(8) ? "" : reader.GetString(8),
                Correo = reader.IsDBNull(9) ? "" : reader.GetString(9),
                Telefono = reader.IsDBNull(10) ? "" : reader.GetString(10),
                Numero_Documento = reader.IsDBNull(11) ? "" : reader.GetString(11),
                Tipo_Documento = reader.IsDBNull(12) ? "" : reader.GetString(12),

                Id_Direccion = reader.IsDBNull(13) ? (int?)null : reader.GetInt32(13),
                Direccion = reader.IsDBNull(14) ? "" : reader.GetString(14),
                Referencia = reader.IsDBNull(15) ? "" : reader.GetString(15),
                Departamento = reader.IsDBNull(16) ? "" : reader.GetString(16),
                Provincia = reader.IsDBNull(17) ? "" : reader.GetString(17),
                Distrito = reader.IsDBNull(18) ? "" : reader.GetString(18),
                CodigoPostal = reader.IsDBNull(19) ? "" : reader.GetString(19)
            };
        }

    }
}
