using AutoMapper;
using Bussines;
using Bussnies;
using Constantes;
using DBModel.DB;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using IBussines;
using IBussnies;
using IRepository;
using IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using SixLabors.ImageSharp;
using System.Text;
using System.Text.Json.Serialization;
using UtilMapper;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

/* PARA IMPLEMENTAR NUESTROS PROTOCOLOS DE SEGURIDAD ==> JWT */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Agregar otros servicios al contenedor.
// Si tu clase est� en Models.Comon:
builder.Services.Configure<Models.Comon.AzureCognitiveServicesSettings>(
    builder.Configuration.GetSection("AzureCognitiveServices"));
builder.Services.AddTransient<IAzureComputerVisionService, AzureComputerVisionService>();
builder.Services.AddControllers();
// Configuraci�n de Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SISTEMA DE LIBRERIA",
        Version = "v1",
        Description = "Documentaci�n de los servicios para el sistema de Libreria Saber",
        Contact = new OpenApiContact
        {
            Name = "Jos� Salazar",
            Email = "i2221915@continental.edu.pe",
            Url = new Uri("https://www.linkedin.com/in/franklinleonciofuamanaraujo/"),
        },
    });
});

builder.Services.AddAutoMapper(typeof(IStartup).Assembly, typeof(AutoMapperProfiles).Assembly);

// Registro de servicios
builder.Services.AddScoped<ILibroBussines, LibroBussines>();
builder.Services.AddScoped<IApisPaypalServices, ApisPaypalServices>();

builder.Services.AddScoped<IKardexRepository, KardexRepository>();
builder.Services.AddScoped<IKardexBussines, KardexBussines>();
builder.Services.AddScoped<IVentaBussines, VentaBussines>();
builder.Services.AddScoped<IDetalleVentaBussines, DetalleVentaBussines>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPersonaBussines, PersonaBussines>();
builder.Services.AddScoped<ICajaBussines, CajaBussines>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IPaymentService,MercadoPagoService>();
builder.Services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();
builder.Services.AddScoped<IPrecioRepository, PrecioRepository>();
builder.Services.AddScoped<IEstadoPedidoBussines, EstadoPedidoBussines>();
builder.Services.AddScoped<IEstadoPedidoImageneBussines, EstadoPedidoImageneBussines>();
builder.Services.AddScoped<IEstadoPedidoRepository, EstadoPedidoRepository>();
builder.Services.AddScoped<IOrderMesageFirebase, OrderMesageFirebase>();
builder.Services.AddScoped<IUsuarioBussnies, UsuarioBussnies>();
builder.Services.AddScoped<ILibroAutorRepository, LibroAutorRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IDireccionRepository, DireccionRepository>();
builder.Services.AddScoped<IDireccionBussines, DireccionBussines>();
builder.Services.AddHttpClient();






var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
