
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Repository;
using Services;
using FluentValidation;
using System.Reflection;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("postgres");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<FacturaRepository>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<FacturaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // agregamos validaciones a nuestro programa
builder.Services.AddFluentValidationAutoValidation(); // ejecuta las validaciones automaticamente y retorna un status 400 por defecto

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
