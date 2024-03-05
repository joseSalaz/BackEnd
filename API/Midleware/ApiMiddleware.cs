using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Comon;
using Models.RequestResponse;

namespace API.Midleware
{
    public class ApiMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHelperHttpContext _helperHttpContext = null;
        private readonly IMapper _mapper;
        //private readonly IErrorBussnies _errorBussnies;


        public ApiMiddleware(RequestDelegate next, IMapper mapper)
        {
            this.next = next;
            _helperHttpContext = new HelperHttpContext();
            _mapper = mapper;
            //_errorBussnies = new ErrorBussnies(mapper);
        }


        public async Task Invoke(HttpContext context)
        {

            //try
            //{

            //    //string codigoAplicacion = context.Request.Headers["codigoAplicacion"].ToString();
            //    //if(codigoAplicacion == null || codigoAplicacion != "456789")
            //    //{
            //    //    throw new Exception("No se envió el código de aplicación correcto");
            //    //}

            //    context.Request.EnableBuffering();
            //    await next(context);
            //}
            //catch (SqlException ex)
            //{
            //    CustomException exx = new CustomException("001", "Error en base de datos");
            //    await HandleExceptionAsync(context, exx);
            //}
            //catch (DbUpdateException ex)
            //{
            //    CustomException exx = new CustomException("002", "Error al actualizar registros");
            //    await HandleExceptionAsync(context, exx);
            //}
            //catch (DivideByZeroException ex)
            //{
            //    CustomException exx = new CustomException("003", "Error de división entre 0");
            //    await HandleExceptionAsync(context, exx);
            //}
            //catch (ArithmeticException ex)
            //{
            //    CustomException exx = new CustomException("004", "Error al hacer algun calculo");
            //    await HandleExceptionAsync(context, exx);
            //}
            //catch (Exception ex)
            //{
            //    CustomException exx = new CustomException("005", "Error no controlado");
            //    await HandleExceptionAsync(context, exx);
            //}
        }

        private Task HandleExceptionAsync(HttpContext context, CustomException ex)
        {
            var controllerActionDescriptor = context.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.ActionName;

            InfoRequest info = _helperHttpContext.GetInfoRequest(context);
            GenericResponse error = new GenericResponse();
            //error = _errorBussnies.Register(ex, info);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            return context.Response.WriteAsJsonAsync(error);

        }
    }
}

