using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IAzureStorage
    {
        Task<string> SaveFile(string containerName, IFormFile file);
    }
}
