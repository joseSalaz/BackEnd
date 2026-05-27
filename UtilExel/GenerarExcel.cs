using ClosedXML.Excel;

namespace UtilExel
{
    public class GenerarExcel
    {
        public static byte[] CrearExcel<T>(List<T> datos, string nombreHoja = "Datos")
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(nombreHoja);
                var propiedades = typeof(T).GetProperties();

                // Escribir encabezados
                for (int i = 0; i < propiedades.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = propiedades[i].Name;
                }

                // Escribir datos
                int row = 2;
                foreach (var item in datos)
                {
                    for (int col = 0; col < propiedades.Length; col++)
                    {
                        var valor = propiedades[col].GetValue(item);
                        worksheet.Cell(row, col + 1).Value = valor?.ToString() ?? "";
                    }
                    row++;
                }

                // Autoajustar columnas
                worksheet.Columns().AdjustToContents();

                // Guardar en un MemoryStream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
