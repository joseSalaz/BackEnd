﻿using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBussines
{
   public interface IAuthBussines : IDisposable
    {
        LoginResponse login(LoginRequest request);

        //Task<LoginResponse> LoginWithGoogle(LoginRequest request);
    }
}