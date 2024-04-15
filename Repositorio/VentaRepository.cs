using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VentaRepository: GenericRepository<Venta>,IVentaRepository
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


        public async Task<(List<Venta>, int)> GetVentaPaginados(int page, int pageSize)
        {
            var query = dbSet.AsQueryable();
            int totalItems = await query.CountAsync();
            var venta = await query.Skip((page-1) * pageSize).Take(pageSize).ToListAsync();
            return (venta, totalItems);
        }


       



    }
}
