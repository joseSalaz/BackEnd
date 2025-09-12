
using DBModel.DB;
using Models.RequestResponse;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;



namespace UtilPDF
{
    public class PdfGenerator
    {
        public static double MeasureTextWidth(XGraphics gfx, string text, XFont font)
        {
            XSize size = gfx.MeasureString(text, font);
            return size.Width;
        }

        public static MemoryStream CreateDetalleVentaPdf(List<DetalleVentaRequest> detalles, Venta venta, Persona persona)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Size = PdfSharpCore.PageSize.A4;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Establecer fuentes con mejor jerarquía visual
            XFont titleFont = new XFont("Arial", 22, XFontStyle.Bold);
            XFont headerFont = new XFont("Arial", 12, XFontStyle.Bold);
            XFont normalFont = new XFont("Arial", 10);
            XFont smallFont = new XFont("Arial", 9);

            // Colores personalizados
            XColor primaryColor = XColor.FromArgb(44, 62, 80);
            XColor secondaryColor = XColor.FromArgb(52, 73, 94);
            XColor tableHeaderColor = XColor.FromArgb(236, 240, 241);

            // Márgenes y dimensiones
            double marginLeft = 50;
            double marginRight = 50;
            double marginTop = 50;
            double pageWidth = page.Width;
            double contentWidth = pageWidth - marginLeft - marginRight;

            // Dibuja el título centrado con línea decorativa
            gfx.DrawString("COMPROBANTE DE COMPRA", titleFont, new XSolidBrush(primaryColor),
                new XRect(0, marginTop, pageWidth, 30), XStringFormats.TopCenter);

            // Línea decorativa bajo el título
            double lineY = marginTop + 35;
            gfx.DrawLine(new XPen(primaryColor, 1),
                new XPoint(marginLeft, lineY),
                new XPoint(pageWidth - marginRight, lineY));

            // Información de la empresa (lado izquierdo)
            double yPos = marginTop + 60;
            gfx.DrawString("LIBRERIA SABER", headerFont, new XSolidBrush(primaryColor), new XPoint(marginLeft, yPos));
            gfx.DrawString("R.U.C: 10200282707", normalFont, new XSolidBrush(secondaryColor), new XPoint(marginLeft, yPos + 20));
            gfx.DrawString("Jr. Huamanmarca N° 255 Huancayo", normalFont, new XSolidBrush(secondaryColor), new XPoint(marginLeft, yPos + 40));
            gfx.DrawString("contacto@empresa.com", normalFont, new XSolidBrush(secondaryColor), new XPoint(marginLeft, yPos + 60));

            // Información del comprobante (lado derecho)
            double rightAlignX = pageWidth - marginRight - 200;
            XRect infoBox = new XRect(rightAlignX, yPos, 200, 80);
            gfx.DrawRectangle(new XPen(primaryColor, 1), infoBox);

            gfx.DrawString("FECHA:", headerFont, new XSolidBrush(primaryColor),
                new XPoint(rightAlignX + 10, yPos + 20));
            gfx.DrawString(venta.FechaVenta?.ToString("dd/MM/yyyy"), normalFont,
                new XSolidBrush(secondaryColor), new XPoint(rightAlignX + 100, yPos + 20));

            gfx.DrawString("N° COMP:", headerFont, new XSolidBrush(primaryColor),
                new XPoint(rightAlignX + 10, yPos + 40));
            gfx.DrawString(venta.NroComprobante, normalFont,
                new XSolidBrush(secondaryColor), new XPoint(rightAlignX + 100, yPos + 40));

            // Información del cliente
            yPos += 100;
            XRect clientBox = new XRect(marginLeft, yPos, contentWidth, 60);
            gfx.DrawRectangle(new XPen(primaryColor, 1), clientBox);

            // Concatenar nombre completo
            string nombreCompleto = $"{persona.Nombre} {persona.ApellidoPaterno} {persona.ApellidoMaterno}";

            // Dibujar el texto en el PDF
            gfx.DrawString("CLIENTE:", headerFont, new XSolidBrush(primaryColor),
                new XPoint(marginLeft + 10, yPos + 15));

            gfx.DrawString(nombreCompleto, normalFont, new XSolidBrush(secondaryColor),
                new XPoint(marginLeft + 80, yPos + 15));

            gfx.DrawString("CORREO:", headerFont, new XSolidBrush(primaryColor),
                new XPoint(marginLeft + 10, yPos + 35));
            gfx.DrawString(persona.Correo, normalFont, new XSolidBrush(secondaryColor),
                new XPoint(marginLeft + 80, yPos + 35));

            // Tabla de productos
            yPos += 80;
            double tableWidth = contentWidth;
            double[] columnWidths = {
            tableWidth * 0.08,  // #
            tableWidth * 0.12,  // Cant.
            tableWidth * 0.45,  // Producto
            tableWidth * 0.17,  // P. Unit.
            tableWidth * 0.18   // Importe
        };

            // Encabezados de la tabla
            XRect headerRect = new XRect(marginLeft, yPos, tableWidth, 25);
            gfx.DrawRectangle(new XSolidBrush(tableHeaderColor), headerRect);
            gfx.DrawRectangle(new XPen(primaryColor, 1), headerRect);

            double xPos = marginLeft;
            string[] headers = { "#", "Cant.", "Producto", "P. Unit.", "Importe" };
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawString(headers[i], headerFont, new XSolidBrush(primaryColor),
                    new XRect(xPos, yPos, columnWidths[i], 25), XStringFormats.Center);
                xPos += columnWidths[i];
            }

            // Contenido de la tabla
            yPos += 25;
            decimal totalGeneral = 0m;
            int itemNumber = 1;

            foreach (var detalle in detalles)
            {
                xPos = marginLeft;
                double rowHeight = 25;
                XRect rowRect = new XRect(marginLeft, yPos, tableWidth, rowHeight);
                gfx.DrawRectangle(new XPen(primaryColor, 0.5), rowRect);

                // Número
                gfx.DrawString(itemNumber.ToString(), normalFont, new XSolidBrush(secondaryColor),
                    new XRect(xPos, yPos, columnWidths[0], rowHeight), XStringFormats.Center);
                xPos += columnWidths[0];

                // Cantidad
                gfx.DrawString(detalle.Cantidad?.ToString() ?? "0", normalFont, new XSolidBrush(secondaryColor),
                    new XRect(xPos, yPos, columnWidths[1], rowHeight), XStringFormats.Center);
                xPos += columnWidths[1];

                // Producto
                gfx.DrawString(detalle.NombreProducto, normalFont, new XSolidBrush(secondaryColor),
                    new XRect(xPos + 5, yPos, columnWidths[2] - 10, rowHeight), XStringFormats.CenterLeft);
                xPos += columnWidths[2];

                // Precio unitario
                gfx.DrawString($"S/ {detalle.PrecioUnit?.ToString("0.00")}", normalFont, new XSolidBrush(secondaryColor),
                    new XRect(xPos, yPos, columnWidths[3], rowHeight), XStringFormats.Center);
                xPos += columnWidths[3];

                // Importe
                decimal subtotal = (detalle.Cantidad ?? 0) * (detalle.PrecioUnit ?? 0);
                gfx.DrawString($"S/ {subtotal.ToString("0.00")}", normalFont, new XSolidBrush(secondaryColor),
                    new XRect(xPos, yPos, columnWidths[4], rowHeight), XStringFormats.Center);

                totalGeneral += subtotal;
                yPos += rowHeight;
                itemNumber++;
            }

            // Total
            XRect totalRect = new XRect(marginLeft + tableWidth - columnWidths[3] - columnWidths[4], yPos,
                columnWidths[3] + columnWidths[4], 30);
            gfx.DrawRectangle(new XSolidBrush(tableHeaderColor), totalRect);
            gfx.DrawRectangle(new XPen(primaryColor, 1), totalRect);

            gfx.DrawString("TOTAL:", headerFont, new XSolidBrush(primaryColor),
                new XRect(totalRect.X, yPos, columnWidths[3], 30), XStringFormats.Center);
            gfx.DrawString($"S/ {totalGeneral.ToString("0.00")}", headerFont, new XSolidBrush(primaryColor),
                new XRect(totalRect.X + columnWidths[3], yPos, columnWidths[4], 30), XStringFormats.Center);

            // Pie de página
            double footerY = page.Height - 50;
            gfx.DrawString("Gracias por su preferencia", smallFont, new XSolidBrush(secondaryColor),
                new XRect(0, footerY, pageWidth, 20), XStringFormats.Center);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;
            return stream;
        }
    }
}
