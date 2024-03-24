﻿using IService;
using Microsoft.AspNetCore.Http;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;


namespace IBussines
{
    public interface ILibroBussines : ICRUDBussnies<LibroRequest,LibroResponse>
    {
        Task<LibroResponse> CreateWithImage(LibroRequest entity, IFormFile imageFile);
    }
}