using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.RequestResponse;
using Models.ResponseResponse;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace IBussines
{
   public interface IAuthBussines : IDisposable
    {
        LoginResponse login(LoginRequest request);

        //Task<LoginResponse> LoginWithGoogle(string credentials);


        
    }
}
