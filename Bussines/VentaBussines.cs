using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepositorio;
using IRepository;
using IService;
using Models.RequestResponse;
using Repository;


using UtilPDF;

namespace Bussines
{

    public class VentaBussines : IVentaBussines

    {
        #region Declaracion de vcariables generales
        public readonly IVentaRepository _IVentaRepository = null;
        public readonly IMapper _Mapper;
        public readonly IEmailService _emailService;

        #endregion

        #region constructor 
        public VentaBussines(IMapper mapper,IEmailService emailService)
        {
            _Mapper = mapper;
            _IVentaRepository = new VentaRepository();
            _emailService = emailService;
        }
        #endregion

        public VentaResponse Create(VentaRequest entity)
        {
            Venta au = _Mapper.Map<Venta>(entity);
            au = _IVentaRepository.Create(au);
            VentaResponse res = _Mapper.Map<VentaResponse>(au);
            return res;
        }

        public List<VentaResponse> CreateMultiple(List<VentaRequest> request)
        {
            List<Venta> au = _Mapper.Map<List<Venta>>(request);
            au = _IVentaRepository.InsertMultiple(au);
            List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IVentaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<VentaRequest> request)
        {
            List<Venta> au = _Mapper.Map<List<Venta>>(request);
            int cantidad = _IVentaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<VentaResponse> getAll()
        {
            List<Venta> lsl = _IVentaRepository.GetAll();
            List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(lsl);
            return res;
        }

        public List<VentaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public VentaResponse getById(object id)
        {
            Venta au = _IVentaRepository.GetById(id);
            VentaResponse res = _Mapper.Map<VentaResponse>(au);
            return res;
        }

        public VentaResponse Update(VentaRequest entity)
        {
            Venta au = _Mapper.Map<Venta>(entity);
            au = _IVentaRepository.Update(au);
            VentaResponse res = _Mapper.Map<VentaResponse>(au);
            return res;
        }

        public List<VentaResponse> UpdateMultiple(List<VentaRequest> request)
        {
            List<Venta> au = _Mapper.Map<List<Venta>>(request);
            au = _IVentaRepository.UpdateMultiple(au);
            List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(au);
            return res;
        }

        public async Task<List<DetalleVenta>> GetDetalleVentaByVentaId(int idVenta)
        {
            return await _IVentaRepository.GetDetallesByVentaId(idVenta);
        }


        public async Task<MemoryStream> CreateVentaPdf(int idVenta)
        {
            // Obtener la venta y sus detalles de venta.
            var result = await _IVentaRepository.GetVentaConDetalles(idVenta);
            var venta = result.venta;
            var detallesVenta = result.detalles;

            if (venta == null || !detallesVenta.Any())
            {
                throw new Exception("No se encontraron datos para la venta.");
            }

            // Obtener la información de la persona asociada con la venta.
            var persona = await _IVentaRepository.GetPersonaByVentaId(idVenta);
            if (persona == null)
            {
                throw new Exception("No se pudo obtener información de la persona para la venta.");
            }

            // Convierte los detalles de venta a DetalleVentaRequest si es necesario.
            List<DetalleVentaRequest> detallesVentaRequest = detallesVenta
                .Select(dv => _Mapper.Map<DetalleVentaRequest>(dv))
                .ToList();

            // Genera el PDF con la información de la venta, los detalles y la persona.
            MemoryStream pdfStream = PdfGenerator.CreateDetalleVentaPdf(detallesVentaRequest, venta, persona);

            return pdfStream;
        }

        public async Task GenerarYEnviarPdfDeVenta(int idVenta, string emailCliente)
        {
            // Suponemos que ya tienes un método que genera el PDF
            MemoryStream pdfStream = await this.CreateVentaPdf(idVenta);

            // Asegúrate de que la posición del stream esté al inicio antes de enviar.
            pdfStream.Position = 0;

            // Aquí asumimos que tienes un método en IEmailService que soporta enviar un stream como adjunto
            await _emailService.SendEmailAsync(
                emailCliente,
                "Tu Boleta de Venta Electrónica",
                "Se adjunta la Bolte Electronica en PDF",
                pdfStream,
                $"BoletaVenta_{idVenta}.pdf"
            );
        }
        public async Task<string> GenerarNumeroComprobante()
        {
            string ultimoComprobante = await _IVentaRepository.ObtenerUltimoNumeroComprobante();

            int numeroActual = 0;
            if (ultimoComprobante != null)
            {
                int.TryParse(ultimoComprobante.Substring(3), out numeroActual);
            }
            numeroActual++;
            return $"BOL{numeroActual.ToString("D4")}";
        }
        public async Task<IEnumerable<VentaRequest>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = await _IVentaRepository.ObtenerVentasPorFechaAsync(fechaInicio, fechaFin);

            return ventas.Select(v => new VentaRequest
            {
                IdVentas = v.IdVentas,
                TotalPrecio = v.TotalPrecio,
                TipoComprobante = v.TipoComprobante,
                FechaVenta = v.FechaVenta,
                NroComprobante = v.NroComprobante,
                IdPersona = v.IdPersona,
                IdUsuario = v.IdUsuario
            });
        }

        public async Task<(List<VentaResponse>, int)> GetVentaPaginados(int page, int pageSize)
        {
            var (ventas, totalItems) = await _IVentaRepository.GetVentaPaginados(page, pageSize);
            var ventaResponse = _Mapper.Map<List<VentaResponse>>(ventas);
            return (ventaResponse, totalItems);
        }


    }
}
