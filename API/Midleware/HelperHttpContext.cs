using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Net.Http.Headers;
using Models.Comon;
using System.Security.Claims;

namespace API.Midleware
{
    public class HelperHttpContext : IHelperHttpContext
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public InfoRequest GetInfoRequest(HttpContext request)
        {
            InfoRequest obj = new InfoRequest();
            obj.Claims = getTokenClaims(request);
            obj.RequestHttp = getHttpContextInfo(request);
            return obj;
        }

        /// <summary>
        /// Retorna todas variables que se le asignaron a los claims al momento de generar el token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private TokenClaims getTokenClaims(HttpContext request)
        {
            TokenClaims obj = new TokenClaims();
            string autorizacion = request.Request.Headers[HeaderNames.Authorization];
            if (autorizacion != null)
            {
                var identity = request.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    obj.Role = identity.Claims.
                        Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                    obj.Nombre =
                        identity.Claims.Where(c => c.Type == "DisplayName").Select(c => c.Value).SingleOrDefault();

                    obj.UserId = int.Parse(identity.Claims.Where(c => c.Type == "UserId").Select(c => c.Value).SingleOrDefault());
                    obj.UserName = identity.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
                    obj.UserName = identity.Claims.Where(c => c.Type == "RoleName").Select(c => c.Value).SingleOrDefault();
                }
            }
            return obj;
        }

        private ApiRequestContext getHttpContextInfo(HttpContext request)
        {
            ApiRequestContext obj = new ApiRequestContext();
            obj.AbsolutePath = request.Request.GetEncodedUrl(); ;
            obj.AbsoluteUri = request.Request.GetEncodedPathAndQuery();
            obj.Ip = request.Connection.RemoteIpAddress.ToString();
            obj.Method = $"{request.Request.Method}";
            obj.UserAgent = request.Request.Headers[HeaderNames.UserAgent];
            obj.Controller = request.GetEndpoint().DisplayName;
            obj.Host = request.Request.Headers[HeaderNames.Host];
            try
            {
                var reader = new StreamReader(request.Request.Body);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                obj.BodyRequest = reader.ReadToEnd();
            }
            catch (Exception)
            {
                obj.BodyRequest = "";
            }
            return obj;
        }


    }
}
