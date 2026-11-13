using Microsoft.EntityFrameworkCore;
using DSW_3P_PR02.Data;
using DSW_3P_PR02.Repositories;
using DSW_3P_PR02.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias
builder.Services.AddScoped<IEventoRepository, EventoSQLRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();
//Configuracion DE SQL Server

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("SqlServerConnection")
    )
);

var app = builder.Build();

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapear los controladores
app.MapControllers();

app.Run();

//ORM
//dotnet add package Microsft.EntityFrameworkCore.SqlServer
//dotnet add package Microsft.EntityFrameworkCore.Design
//dotnet tool install --global dotnet-ef --version 10.0.*