
using AdminLibrary.Admin.Core;
using AdminLibrary.Admin.Infrastructure;
using AdminLibrary.Models;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AdminLibrary.Admin.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            /// Agregar DbContext con cadena de conexión
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            //Agregar controladores 
            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca Digital Arquitectura Hexagonal", Version = "v1" });
            });

            ///Se oculta la consola de Lifetime
            builder.Logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.None);

            //Agregar Servicios
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                //Core
                _ = container.RegisterModule(new DefaultCoreModule());
                _ = container.RegisterModule(new DefaultInfrastructureModule());
            });

            WebApplication app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseShowAllServicesMiddleware();
            }
            else
            {
                _ = app.UseExceptionHandler("/Home/Error");
                _ = app.UseHsts();  
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca Digital Arquitectura Limpia");
            });

            app.MapControllers();

            app.Run();
        }
    }
}
