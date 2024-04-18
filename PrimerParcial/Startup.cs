
using Services.Logica;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;

namespace PrimerParcial;

public class Startup
{
    public static WebApplication InitializeApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        ConfigureServices(builder, configuration);
        var app = builder.Build();
        Configure(app);
        return app;
    }

    private static void ConfigureServices(WebApplicationBuilder builder, IConfiguration configuration)
    {

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Agrega tus servicios personalizados aquí
        builder.Services.AddScoped<ClienteService>(provider => // Agrega tu servicio OperationService
        {
            var connectionString = configuration.GetConnectionString("postgresDB");
            return new ClienteService(connectionString);
        });


        
        
        
    }



    private static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}
