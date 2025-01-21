using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialPropuestaController : ControllerBase
    {
        private readonly ProyectoDAWAContext _context;

        public HistorialPropuestaController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: api/HistorialPropuesta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialPropuesta>>> GetHistorialPropuestas()
        {
            return await _context.HistorialPropuestas.ToListAsync();
        }

        // GET: api/HistorialPropuesta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialPropuesta>> GetHistorialPropuesta(int id)
        {
            var historialPropuesta = await _context.HistorialPropuestas.FindAsync(id);

            if (historialPropuesta == null)
            {
                return NotFound();
            }

            return historialPropuesta;
        }

        // PUT: api/HistorialPropuesta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialPropuesta(int id, HistorialPropuesta historialPropuesta)
        {
            if (id != historialPropuesta.Id)
            {
                return BadRequest();
            }

            _context.Entry(historialPropuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialPropuestaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HistorialPropuesta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialPropuesta>> PostHistorialPropuesta(HistorialPropuesta historialPropuesta)
        {
            _context.HistorialPropuestas.Add(historialPropuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialPropuesta", new { id = historialPropuesta.Id }, historialPropuesta);
        }

        // DELETE: api/HistorialPropuesta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialPropuesta(int id)
        {
            var historialPropuesta = await _context.HistorialPropuestas.FindAsync(id);
            if (historialPropuesta == null)
            {
                return NotFound();
            }

            _context.HistorialPropuestas.Remove(historialPropuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/HistorialPropuesta/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Verificar si el archivo es un PDF
            if (file.ContentType != "application/pdf")
            {
                return BadRequest("Solo se permiten archivos PDF.");
            }

            // Define la carpeta donde se almacenarán los archivos (dentro del proyecto)
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            // Crear la carpeta si no existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generar un nombre único para el archivo (para evitar sobreescribir archivos)
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);

            // Guardar el archivo en la carpeta
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Crear el registro en la base de datos con la ruta del archivo
            var fileRecord = new HistorialPropuesta
            {
                DireccionArchivo = filePath // Guardar la ruta del archivo
            };

            // Guardar en la base de datos
            _context.HistorialPropuestas.Add(fileRecord);
            await _context.SaveChangesAsync();

            return Ok(new { FilePath = filePath });
        }

        private bool HistorialPropuestaExists(int id)
        {
            return _context.HistorialPropuestas.Any(e => e.Id == id);
        }


    }
}
