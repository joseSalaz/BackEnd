using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularDev",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });


        // Configuración de otros servicios
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        // Otros middlewares

        app.UseCors("AllowAngularDev");

        app.UseExceptionHandler(builder =>
        {
            builder.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                await next();
            });
        });
    }
}
