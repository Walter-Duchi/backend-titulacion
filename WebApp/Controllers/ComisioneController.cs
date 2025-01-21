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
    public class ComisioneController : ControllerBase
    {
        private readonly ProyectoDAWAContext _context;

        public ComisioneController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: api/Comisione
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comisione>>> GetComisiones()
        {
            return await _context.Comisiones.ToListAsync();
        }

        // GET: api/Comisione/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comisione>> GetComisione(int id)
        {
            var comisione = await _context.Comisiones.FindAsync(id);

            if (comisione == null)
            {
                return NotFound();
            }

            return comisione;
        }

        // PUT: api/Comisione/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComisione(int id, Comisione comisione)
        {
            if (id != comisione.Id)
            {
                return BadRequest();
            }

            _context.Entry(comisione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComisioneExists(id))
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

        // POST: api/Comisione
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comisione>> PostComisione(Comisione comisione)
        {
            _context.Comisiones.Add(comisione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComisione", new { id = comisione.Id }, comisione);
        }

        // DELETE: api/Comisione/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComisione(int id)
        {
            var comisione = await _context.Comisiones.FindAsync(id);
            if (comisione == null)
            {
                return NotFound();
            }

            _context.Comisiones.Remove(comisione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComisioneExists(int id)
        {
            return _context.Comisiones.Any(e => e.Id == id);
        }
    }
}
