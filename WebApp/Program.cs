using Microsoft.EntityFrameworkCore;
using ProyectoDAWA.Repositories;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregar controladores para la API
builder.Services.AddControllers();

// Registrar el DbContext con la cadena de conexi�n desde appsettings.json
builder.Services.AddDbContext<ProyectoDAWAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar los repositorios
builder.Services.AddScoped<IPropuestaRepository, PropuestaRepository>();
builder.Services.AddScoped<IComisionRepository, ComisionRepository>();
builder.Services.AddScoped<IEstudiantesPropuestaRepository, EstudiantesPropuestaRepository>();
builder.Services.AddScoped<IHistorialPropuestaRepository, HistorialPropuestaRepository>();
builder.Services.AddScoped<IMiembrosComisionRepository, MiembrosComisionRepository>();

// Si tienes m�s repositorios, reg�stralos aqu� de la misma forma

// Registrar Swagger para la documentaci�n de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de autenticaci�n (si usas JWT o alguna otra forma de autenticaci�n)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001"; // Reemplaza con la URL de tu servicio de autenticaci�n
        options.Audience = "api1"; // Reemplaza con tu API
        options.RequireHttpsMetadata = true;
    });

// Configuraci�n de CORS (Permitir todas las solicitudes desde cualquier origen)
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

// Habilitar la autenticaci�n
app.UseAuthentication();

// Habilitar la autorizaci�n
app.UseAuthorization();

// Habilitar CORS si est� configurado
app.UseCors("AllowAll");

// Mapeo de los controladores de la API
app.MapControllers();

// Ejecutar la aplicaci�n
app.Run();
