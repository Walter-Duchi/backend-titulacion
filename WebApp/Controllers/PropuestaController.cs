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
    public class PropuestaController : ControllerBase
    {
        private readonly ProyectoDAWAContext _context;

        public PropuestaController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: api/Propuesta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propuesta>>> GetPropuestas()
        {
            return await _context.Propuestas.ToListAsync();
        }

        // GET: api/Propuesta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Propuesta>> GetPropuesta(int id)
        {
            var propuesta = await _context.Propuestas.FindAsync(id);

            if (propuesta == null)
            {
                return NotFound();
            }

            return propuesta;
        }

        // PUT: api/Propuesta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropuesta(int id, Propuesta propuesta)
        {
            if (id != propuesta.Id)
            {
                return BadRequest();
            }

            _context.Entry(propuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropuestaExists(id))
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

        // POST: api/Propuesta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Propuesta>> PostPropuesta(Propuesta propuesta)
        {
            _context.Propuestas.Add(propuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPropuesta", new { id = propuesta.Id }, propuesta);
        }

        // DELETE: api/Propuesta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropuesta(int id)
        {
            var propuesta = await _context.Propuestas.FindAsync(id);
            if (propuesta == null)
            {
                return NotFound();
            }

            _context.Propuestas.Remove(propuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropuestaExists(int id)
        {
            return _context.Propuestas.Any(e => e.Id == id);
        }
    }
}
