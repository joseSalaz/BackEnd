using Models.RequestResponse;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Reflection.Metadata;

namespace UtilPDF
{
    public class PdfGenerator
    {
        public static MemoryStream CreateDetalleVentaPdf(List<DetalleVentaRequest> detalles)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Establecer fuentes
            XFont titleFont = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont headerFont = new XFont("Verdana", 14, XFontStyle.Bold);
            XFont normalFont = new XFont("Verdana", 12);

            // Dibuja el título de la boleta
            gfx.DrawString("BOLETA DE VENTA ELECTRÓNICA", titleFont, XBrushes.Black,
                new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);

            // Dibuja el encabezado de la empresa
            gfx.DrawString("EMPRESA PRUEBA", headerFont, XBrushes.Black, new XPoint(40, 100));
            gfx.DrawString("R.U.C: 123456789", normalFont, XBrushes.Black, new XPoint(40, 120));
            gfx.DrawString("Dirección: Calle Falsa 123", normalFont, XBrushes.Black, new XPoint(40, 140));
            gfx.DrawString("Correo: contacto@empresa.com", normalFont, XBrushes.Black, new XPoint(40, 160));

            // Dibuja la cabecera de la tabla
            gfx.DrawString("Cant.", headerFont, XBrushes.Black, new XPoint(40, 200));
            gfx.DrawString("Descripción", headerFont, XBrushes.Black, new XPoint(80, 200));
            gfx.DrawString("P. Unit.", headerFont, XBrushes.Black, new XPoint(220, 200));
            gfx.DrawString("Total", headerFont, XBrushes.Black, new XPoint(340, 200));

            // Dibuja los items de la boleta
            int y = 220; // Posición inicial en Y para los items
            foreach (var item in detalles)
            {
                gfx.DrawString(item.Cantidad.ToString(), normalFont, XBrushes.Black, new XPoint(40, y));
                gfx.DrawString(item.NombreProducto, normalFont, XBrushes.Black, new XPoint(80, y));
                // Asegúrate de que PrecioUnit e Importe no sean null antes de llamar a ToString
                gfx.DrawString($"S/ {item.PrecioUnit?.ToString("0.00")}", normalFont, XBrushes.Black, new XPoint(220, y)); // Con dos decimales
                gfx.DrawString($"S/ {item.Importe?.ToString("0.00")}", normalFont, XBrushes.Black, new XPoint(340, y)); // Con dos decimales
                y += 20; // Incrementa la posición en Y para el siguiente item
            }

            // Dibuja el total
            gfx.DrawString("Total", headerFont, XBrushes.Black, new XPoint(220, y + 20));
            gfx.DrawString($"S/ {detalles.Sum(i => i.Importe ?? 0).ToString("0.00")}", headerFont, XBrushes.Black, new XPoint(340, y + 20)); // Con dos decimales



            // Dibuja observaciones y código QR al final de la boleta
            gfx.DrawString("Observaciones: ", normalFont, XBrushes.Black, new XPoint(40, y + 60));
            // Añadir código para generar y dibujar el código QR aquí

            // Guardar el PDF en un MemoryStream y retornar
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            return stream;
        }
    }
}
