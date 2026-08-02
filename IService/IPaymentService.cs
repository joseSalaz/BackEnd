
using MercadoPago.Resource.Payment;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(DatalleCarrito detalleCarrito, decimal total);
    }
}

