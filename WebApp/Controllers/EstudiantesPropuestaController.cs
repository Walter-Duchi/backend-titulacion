using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EstudiantesPropuestaController : Controller
    {
        private readonly ProyectoDAWAContext _context;

        public EstudiantesPropuestaController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: EstudiantesPropuesta
        public async Task<IActionResult> Index()
        {
            var proyectoDAWAContext = _context.EstudiantesPropuestas.Include(e => e.Estudiante).Include(e => e.Propuesta);
            return View(await proyectoDAWAContext.ToListAsync());
        }

        // GET: EstudiantesPropuesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantesPropuesta = await _context.EstudiantesPropuestas
                .Include(e => e.Estudiante)
                .Include(e => e.Propuesta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiantesPropuesta == null)
            {
                return NotFound();
            }

            return View(estudiantesPropuesta);
        }

        // GET: EstudiantesPropuesta/Create
        public IActionResult Create()
        {
            ViewData["EstudianteId"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id");
            return View();
        }

        // POST: EstudiantesPropuesta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstudianteId,PropuestaId")] EstudiantesPropuesta estudiantesPropuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiantesPropuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Usuarios, "Id", "Id", estudiantesPropuesta.EstudianteId);
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id", estudiantesPropuesta.PropuestaId);
            return View(estudiantesPropuesta);
        }

        // GET: EstudiantesPropuesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantesPropuesta = await _context.EstudiantesPropuestas.FindAsync(id);
            if (estudiantesPropuesta == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Usuarios, "Id", "Id", estudiantesPropuesta.EstudianteId);
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id", estudiantesPropuesta.PropuestaId);
            return View(estudiantesPropuesta);
        }

        // POST: EstudiantesPropuesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstudianteId,PropuestaId")] EstudiantesPropuesta estudiantesPropuesta)
        {
            if (id != estudiantesPropuesta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiantesPropuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudiantesPropuestaExists(estudiantesPropuesta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Usuarios, "Id", "Id", estudiantesPropuesta.EstudianteId);
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id", estudiantesPropuesta.PropuestaId);
            return View(estudiantesPropuesta);
        }

        // GET: EstudiantesPropuesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiantesPropuesta = await _context.EstudiantesPropuestas
                .Include(e => e.Estudiante)
                .Include(e => e.Propuesta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiantesPropuesta == null)
            {
                return NotFound();
            }

            return View(estudiantesPropuesta);
        }

        // POST: EstudiantesPropuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiantesPropuesta = await _context.EstudiantesPropuestas.FindAsync(id);
            if (estudiantesPropuesta != null)
            {
                _context.EstudiantesPropuestas.Remove(estudiantesPropuesta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudiantesPropuestaExists(int id)
        {
            return _context.EstudiantesPropuestas.Any(e => e.Id == id);
        }
    }
}
