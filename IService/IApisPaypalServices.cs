using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using PayPal.Api;

namespace IService
{
    public interface IApisPaypalServices
    {
        Task<Payment> CreateOrdersasync(DatalleCarrito deCarrito, decimal amount, string returnUrl, string cancelUrl);
    }
}
