using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class FacturaRequest
    {
        public ClienteRequest Cliente { get; set; }
        public List<ItemRequest> Items { get; set; }
        public string FormaDePago { get; set; }
    }

    public class ClienteRequest
    {
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; } = new Address
        {
            Street = "Default",
            Exterior = "N/A",
            Neighborhood = "N/A",
            Zip = "00000"
        };
    }

    public class Address
    {
        public string Street { get; set; }
        public string Exterior { get; set; }
        public string Neighborhood { get; set; }
        public string Zip { get; set; }
    }

    public class ItemRequest
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
    }
}
