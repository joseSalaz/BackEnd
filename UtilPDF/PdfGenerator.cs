
using DBModel.DB;
using Models.RequestResponse;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;



namespace UtilPDF
{
    public class PdfGenerator
    {
        // Función auxiliar para medir el ancho del texto
        public static double MeasureTextWidth(XGraphics gfx, string text, XFont font)
        {
            XSize size = gfx.MeasureString(text, font);
            return size.Width;
        }

        public static MemoryStream CreateDetalleVentaPdf(List<DetalleVentaRequest> detalles, Venta venta, Persona persona)
        {
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Establecer fuentes
                XFont titleFont = new XFont("Times New Roman", 20, XFontStyle.Bold);
                XFont headerFont = new XFont("Arial", 14, XFontStyle.Bold);
                XFont normalFont = new XFont("Arial", 12);

                // Dibuja el título de la boleta
                gfx.DrawString("BOLETA DE VENTA ELECTRÓNICA", titleFont, XBrushes.Black,
                    new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);

                // Dibuja el encabezado de la empresa
                gfx.DrawString("LIBRERIA SABER", headerFont, XBrushes.Black, new XPoint(40, 100));
                gfx.DrawString("R.U.C: 10200282707", normalFont, XBrushes.Black, new XPoint(40, 120));
                gfx.DrawString("Dirección: Jr. Huamanmarca N° 255 Huancayo", normalFont, XBrushes.Black, new XPoint(40, 140));
                gfx.DrawString("Correo: contacto@empresa.com", normalFont, XBrushes.Black, new XPoint(40, 160));
                gfx.DrawString($"Nombre del Cliente: {persona.Nombre}", normalFont, XBrushes.Black, new XPoint(40, 180));
                gfx.DrawString($"Correo del Cliente: {persona.Correo}", normalFont, XBrushes.Black, new XPoint(40, 200));

                // Coordenada X para la fecha y el número de comprobante, ajustar según sea necesario para alinearlo a la derecha
                int xCoordinateForDateAndNumber = 400;

                // Alinea 'Fecha de Venta' con 'LIBRERIA SABER'
                gfx.DrawString($"Fecha de Venta: {venta.FechaVenta?.ToString("dd/MM/yyyy")}", normalFont, XBrushes.Black, new XPoint(xCoordinateForDateAndNumber, 100));

                // Alinea 'Nro. Comprobante' con 'R.U.C:'
                gfx.DrawString($"Nro. Comprobante: {venta.NroComprobante}", normalFont, XBrushes.Black, new XPoint(xCoordinateForDateAndNumber, 120));

                // Configurar márgenes y dimensiones de la tabla
                double marginLeft = 40;
                double marginTop = 220; // Asumiendo que hay contenido antes de la tabla
                double tableWidth = page.Width - marginLeft * 2; // El ancho de la tabla es el ancho de la página menos los márgenes izquierdo y derecho
                double numberColumnWidth = tableWidth * 0.05; // Ancho para el número de ítem
                double columnWidth1 = tableWidth * 0.10; // Cant.
                double columnWidth2 = tableWidth * 0.50; // Descripción
                double columnWidth3 = tableWidth * 0.20; // P. Unit.
                double columnWidth4 = tableWidth * 0.15; // Total
                double rowHeight = 20;

                // Calcular la altura total de la tabla basada en el número de detalles
                double tableHeight = detalles.Count * rowHeight + rowHeight; // Agrega espacio para el encabezado
                                                                             // Actualización de los anchos de las columnas aquí



                // Dibujar la cabecera de la tabla
                double x = marginLeft;
                gfx.DrawString("#", headerFont, XBrushes.Black, new XRect(x, marginTop, numberColumnWidth, rowHeight), XStringFormats.TopLeft);
                x += numberColumnWidth;
                gfx.DrawString("Cant.", headerFont, XBrushes.Black, new XRect(x, marginTop, columnWidth1, rowHeight), XStringFormats.TopLeft);
                x += columnWidth1;
                gfx.DrawString("Producto", headerFont, XBrushes.Black, new XRect(x, marginTop, columnWidth2, rowHeight), XStringFormats.TopLeft);
                x += columnWidth2;
                gfx.DrawString("P. Unit.", headerFont, XBrushes.Black, new XRect(x, marginTop, columnWidth3, rowHeight), XStringFormats.TopLeft);
                x += columnWidth3;
                gfx.DrawString("Importe", headerFont, XBrushes.Black, new XRect(x, marginTop, columnWidth4, rowHeight), XStringFormats.TopLeft);

                // Dibujar las líneas de la tabla
                gfx.DrawRectangle(XPens.Black, marginLeft, marginTop, tableWidth, tableHeight); // Borde exterior
                gfx.DrawLine(XPens.Black, marginLeft, marginTop + rowHeight, marginLeft + tableWidth, marginTop + rowHeight); // Línea debajo de los encabezados

                // Dibujar las líneas verticales
                x = marginLeft;
                gfx.DrawLine(XPens.Black, x, marginTop, x, marginTop + tableHeight); // Línea vertical inicial
                x += numberColumnWidth;
                gfx.DrawLine(XPens.Black, x, marginTop, x, marginTop + tableHeight); // Línea vertical para número de ítem
                x += columnWidth1;
                gfx.DrawLine(XPens.Black, x, marginTop, x, marginTop + tableHeight); // Línea vertical para cantidad
                x += columnWidth2;
                gfx.DrawLine(XPens.Black, x, marginTop, x, marginTop + tableHeight); // Línea vertical para producto
                x += columnWidth3;
                gfx.DrawLine(XPens.Black, x, marginTop, x, marginTop + tableHeight); // Línea vertical para precio unitario

                // Dibujar los datos de cada fila
                double y = marginTop + rowHeight; // Comenzar debajo de los encabezados
                int itemNumber = 1; // Contador para el número de ítem
                decimal totalGeneral = 0m;


                // Asumiendo que ya tienes 'maxProductWidth' definido como el ancho máximo para la columna de productos.
                double maxProductWidth = tableWidth * 0.50; // Ajusta esto según tus necesidades.

                // Encuentra el ancho de producto más largo, pero no permitas que sea mayor que 'maxProductWidth'.
                double maxActualProductWidth = detalles
                    .Select(d => MeasureTextWidth(gfx, d.NombreProducto, normalFont))
                    .Max();
                maxActualProductWidth = Math.Min(maxActualProductWidth, maxProductWidth);

                // Ajusta las anchuras de las otras columnas si es necesario
                columnWidth2 = maxActualProductWidth; // Actualiza el ancho de la columna de productos al valor calculado
                columnWidth3 = tableWidth * 0.20; // Puedes ajustar este valor según sea necesario
                columnWidth4 = tableWidth * 0.15; // Puedes ajustar este valor según sea necesario

                // Asegúrate de que la suma de todas las anchuras de las columnas no sea mayor que el ancho total de la tabla
                double totalColumnsWidth = numberColumnWidth + columnWidth1 + columnWidth2 + columnWidth3 + columnWidth4;
                if (totalColumnsWidth > tableWidth)
                {
                    // Si excede, ajusta las columnas proporcionalmente o maneja el error como prefieras.
                    // Este código es solo un ejemplo de cómo podrías manejar un ancho excesivo.
                    double scale = tableWidth / totalColumnsWidth;
                    numberColumnWidth *= scale;
                    columnWidth1 *= scale;
                    columnWidth2 *= scale; // Esta columna ya está ajustada, pero la incluimos por consistencia.
                    columnWidth3 *= scale;
                    columnWidth4 *= scale;
                }

                // Ahora dibuja los detalles de la tabla con las anchuras ajustadas
                foreach (var detalle in detalles)
                {
                    x = marginLeft;
                    gfx.DrawString(itemNumber.ToString(), normalFont, XBrushes.Black, new XRect(x, y, numberColumnWidth, rowHeight), XStringFormats.Center);
                    x += numberColumnWidth;

                    gfx.DrawString(detalle.Cantidad?.ToString() ?? "0", normalFont, XBrushes.Black, new XRect(x, y, columnWidth1, rowHeight), XStringFormats.Center);
                    x += columnWidth1;

                    gfx.DrawString(detalle.NombreProducto, normalFont, XBrushes.Black, new XRect(x, y, columnWidth2, rowHeight), XStringFormats.TopLeft);
                    x += columnWidth2;

                    gfx.DrawString($"S/ {detalle.PrecioUnit?.ToString("0.00")}", normalFont, XBrushes.Black, new XRect(x, y, columnWidth3, rowHeight), XStringFormats.Center);
                    x += columnWidth3;

                    var subtotal = (detalle.Cantidad ?? 0) * (detalle.PrecioUnit ?? 0);
                    gfx.DrawString($"S/ {subtotal.ToString("0.00")}", normalFont, XBrushes.Black, new XRect(x, y, columnWidth4, rowHeight), XStringFormats.Center);

                    // No incrementes 'x' aquí porque vamos a reiniciar a 'marginLeft' para la próxima fila.
                    y += rowHeight;
                    itemNumber++;
                
                    
                    decimal cantidad = detalle.Cantidad ?? 0m;
                    decimal precioUnitario = detalle.PrecioUnit ?? 0m;
                    totalGeneral += subtotal;
                }

                // Dibujar la línea debajo de la última fila
                gfx.DrawLine(XPens.Black, marginLeft, y, marginLeft + tableWidth, y);

                // Calcula la posición X donde debería comenzar la columna "P. Unit."
                double pUnitColumnStartX = marginLeft + numberColumnWidth + columnWidth1 + columnWidth2;

                // Calcula la posición X donde debería comenzar la columna "Importe"
                double importeColumnStartX = pUnitColumnStartX + columnWidth3;

                // Dibuja la etiqueta "Total" debajo de "P. Unit."
                gfx.DrawString("Total", headerFont, XBrushes.Black, new XRect(pUnitColumnStartX, y, columnWidth3, rowHeight), XStringFormats.Center);
                gfx.DrawString($"S/ {totalGeneral.ToString("0.00")}", headerFont, XBrushes.Black, new XRect(importeColumnStartX, y, columnWidth4, rowHeight), XStringFormats.Center);

                // Guardar el PDF en un MemoryStream y retornar
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                stream.Position = 0;

                return stream;
            }
        }
    }
}
