using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<string> UploadPedidosImageAsync(IFormFile image);
        Task DeleteFileAsync(string fileUrl);
    }
}
