using AutoMapper;
using Bussines;
using Constantes;
using DBModel.DB;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using IBussines;
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
            Url = new Uri("https://www.linkedin.com/in/franklinleonciofuamanaraujo/"),
        },
    });
});

builder.Services.AddAutoMapper(typeof(IStartup).Assembly, typeof(AutoMapperProfiles).Assembly);

// Registro de servicios
builder.Services.AddScoped<ILibroBussines, LibroBussines>();
builder.Services.AddScoped<IAzureStorage, AzureStorage>();
builder.Services.AddScoped<IApisPaypalServices, ApisPaypalServices>();

builder.Services.AddScoped<IKardexRepository, KardexRepository>();
builder.Services.AddScoped<IKardexBussines, KardexBussines>();
builder.Services.AddScoped<IVentaBussines, VentaBussines>();
builder.Services.AddScoped<IDetalleVentaBussines, DetalleVentaBussines>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPersonaBussines, PersonaBussines>();
builder.Services.AddScoped<ICajaBussines, CajaBussines>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();







var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
