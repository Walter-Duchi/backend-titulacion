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
    public class MiembrosComisionController : ControllerBase
    {
        private readonly ProyectoDAWAContext _context;

        public MiembrosComisionController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: api/MiembrosComision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MiembrosComision>>> GetMiembrosComisions()
        {
            return await _context.MiembrosComisions.ToListAsync();
        }

        // GET: api/MiembrosComision/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MiembrosComision>> GetMiembrosComision(int id)
        {
            var miembrosComision = await _context.MiembrosComisions.FindAsync(id);

            if (miembrosComision == null)
            {
                return NotFound();
            }

            return miembrosComision;
        }

        // PUT: api/MiembrosComision/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMiembrosComision(int id, MiembrosComision miembrosComision)
        {
            if (id != miembrosComision.Id)
            {
                return BadRequest();
            }

            _context.Entry(miembrosComision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MiembrosComisionExists(id))
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

        // POST: api/MiembrosComision
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MiembrosComision>> PostMiembrosComision(MiembrosComision miembrosComision)
        {
            _context.MiembrosComisions.Add(miembrosComision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMiembrosComision", new { id = miembrosComision.Id }, miembrosComision);
        }

        // DELETE: api/MiembrosComision/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMiembrosComision(int id)
        {
            var miembrosComision = await _context.MiembrosComisions.FindAsync(id);
            if (miembrosComision == null)
            {
                return NotFound();
            }

            _context.MiembrosComisions.Remove(miembrosComision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MiembrosComisionExists(int id)
        {
            return _context.MiembrosComisions.Any(e => e.Id == id);
        }
    }
}
