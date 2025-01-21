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
    public class PeriodoController : ControllerBase
    {
        private readonly ProyectoDAWAContext _context;

        public PeriodoController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: api/Periodo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Periodo>>> GetPeriodos()
        {
            return await _context.Periodos.ToListAsync();
        }

        // GET: api/Periodo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Periodo>> GetPeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);

            if (periodo == null)
            {
                return NotFound();
            }

            return periodo;
        }

        // PUT: api/Periodo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriodo(int id, Periodo periodo)
        {
            if (id != periodo.Id)
            {
                return BadRequest();
            }

            _context.Entry(periodo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodoExists(id))
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

        // POST: api/Periodo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Periodo>> PostPeriodo(Periodo periodo)
        {
            _context.Periodos.Add(periodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeriodo", new { id = periodo.Id }, periodo);
        }

        // DELETE: api/Periodo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }

            _context.Periodos.Remove(periodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeriodoExists(int id)
        {
            return _context.Periodos.Any(e => e.Id == id);
        }
    }
}
