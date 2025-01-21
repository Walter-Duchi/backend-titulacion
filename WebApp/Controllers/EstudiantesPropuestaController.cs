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
    public class EstudiantesPropuestaController : ControllerBase
    {
        private readonly ProyectoDAWAContext _context;

        public EstudiantesPropuestaController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: api/EstudiantesPropuesta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudiantesPropuesta>>> GetEstudiantesPropuestas()
        {
            return await _context.EstudiantesPropuestas.ToListAsync();
        }

        // GET: api/EstudiantesPropuesta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstudiantesPropuesta>> GetEstudiantesPropuesta(int id)
        {
            var estudiantesPropuesta = await _context.EstudiantesPropuestas.FindAsync(id);

            if (estudiantesPropuesta == null)
            {
                return NotFound();
            }

            return estudiantesPropuesta;
        }

        // PUT: api/EstudiantesPropuesta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiantesPropuesta(int id, EstudiantesPropuesta estudiantesPropuesta)
        {
            if (id != estudiantesPropuesta.Id)
            {
                return BadRequest();
            }

            _context.Entry(estudiantesPropuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudiantesPropuestaExists(id))
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

        // POST: api/EstudiantesPropuesta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstudiantesPropuesta>> PostEstudiantesPropuesta(EstudiantesPropuesta estudiantesPropuesta)
        {
            _context.EstudiantesPropuestas.Add(estudiantesPropuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudiantesPropuesta", new { id = estudiantesPropuesta.Id }, estudiantesPropuesta);
        }

        // DELETE: api/EstudiantesPropuesta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiantesPropuesta(int id)
        {
            var estudiantesPropuesta = await _context.EstudiantesPropuestas.FindAsync(id);
            if (estudiantesPropuesta == null)
            {
                return NotFound();
            }

            _context.EstudiantesPropuestas.Remove(estudiantesPropuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudiantesPropuestaExists(int id)
        {
            return _context.EstudiantesPropuestas.Any(e => e.Id == id);
        }
    }
}
