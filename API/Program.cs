using API.Extensions;
using AutoMapper;
using Bussines;
using Bussnies;
using Constantes;
using DBModel.DB;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using IBussines;
using IBussnies;
using IRepositorio;
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
using UnitOfWork;
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
// Si tu clase está en Models.Comon:
builder.Services.Configure<Models.Comon.AzureCognitiveServicesSettings>(
    builder.Configuration.GetSection("AzureCognitiveServices"));
builder.Services.AddTransient<IAzureComputerVisionService, AzureComputerVisionService>();
builder.Services.AddControllers();
// Configuración de Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "SISTEMA DE LIBRERIA",
    Version = "v1",
    Description = "Documentación de los servicios para el sistema de Libreria Saber",
    Contact = new OpenApiContact
    {
      Name = "José Salazar",
      Email = "i2221915@continental.edu.pe",
      Url = new Uri("https://www.linkedin.com/in/jose-alberto-salazar-chirinos-3b1bb6297/"),
    },
  });
  var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
  c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDbContext<LibreriaSaberContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddProyectDependencies();

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
