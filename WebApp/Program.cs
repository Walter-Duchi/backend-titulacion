using Microsoft.EntityFrameworkCore;
using ProyectoDAWA.Repositories;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Registrar el DbContext con la cadena de conexión desde appsettings.json
builder.Services.AddDbContext<ProyectoDAWAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar los repositorios
builder.Services.AddScoped<IPropuestaRepository, PropuestaRepository>();
builder.Services.AddScoped<IComisionRepository, ComisionRepository>();
builder.Services.AddScoped<IEstudiantesPropuestaRepository, EstudiantesPropuestaRepository>();
builder.Services.AddScoped<IHistorialPropuestaRepository, HistorialPropuestaRepository>();
builder.Services.AddScoped<IMiembrosComisionRepository, MiembrosComisionRepository>();

// Si tienes más repositorios, regístralos aquí de la misma forma

// Registrar Swagger para la documentación de la API
builder.Services.AddOpenApi();

// Si usas autenticación, también debes configurarla aquí (opcional)
builder.Services.AddAuthentication();

// Configuración de CORS, si se requiere
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll"); // Habilitar CORS si se configuró

app.MapControllers();

app.Run();
