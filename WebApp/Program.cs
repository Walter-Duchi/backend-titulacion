using Microsoft.EntityFrameworkCore;
using ProyectoDAWA.Repositories;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregar controladores para la API
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de autenticación (si usas JWT o alguna otra forma de autenticación)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001"; // Reemplaza con la URL de tu servicio de autenticación
        options.Audience = "api1"; // Reemplaza con tu API
        options.RequireHttpsMetadata = true;
    });

// Configuración de CORS (Permitir todas las solicitudes desde cualquier origen)
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
    // Habilitar Swagger solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar la autenticación
app.UseAuthentication();

// Habilitar la autorización
app.UseAuthorization();

// Habilitar CORS si está configurado
app.UseCors("AllowAll");

// Mapeo de los controladores de la API
app.MapControllers();

// Ejecutar la aplicación
app.Run();
