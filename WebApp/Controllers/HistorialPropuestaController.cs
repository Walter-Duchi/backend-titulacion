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
    public class HistorialPropuestaController : Controller
    {
        private readonly ProyectoDAWAContext _context;

        public HistorialPropuestaController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: HistorialPropuesta
        public async Task<IActionResult> Index()
        {
            var proyectoDAWAContext = _context.HistorialPropuestas.Include(h => h.Propuesta);
            return View(await proyectoDAWAContext.ToListAsync());
        }

        // GET: HistorialPropuesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPropuesta = await _context.HistorialPropuestas
                .Include(h => h.Propuesta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialPropuesta == null)
            {
                return NotFound();
            }

            return View(historialPropuesta);
        }

        // GET: HistorialPropuesta/Create
        public IActionResult Create()
        {
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id");
            return View();
        }

        // POST: HistorialPropuesta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DireccionArchivo,FechaEnvio,EstadoAprobacion,ObservacionRevisor,ComentarioEstudiante,PropuestaId")] HistorialPropuesta historialPropuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialPropuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id", historialPropuesta.PropuestaId);
            return View(historialPropuesta);
        }

        // GET: HistorialPropuesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPropuesta = await _context.HistorialPropuestas.FindAsync(id);
            if (historialPropuesta == null)
            {
                return NotFound();
            }
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id", historialPropuesta.PropuestaId);
            return View(historialPropuesta);
        }

        // POST: HistorialPropuesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DireccionArchivo,FechaEnvio,EstadoAprobacion,ObservacionRevisor,ComentarioEstudiante,PropuestaId")] HistorialPropuesta historialPropuesta)
        {
            if (id != historialPropuesta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialPropuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialPropuestaExists(historialPropuesta.Id))
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
            ViewData["PropuestaId"] = new SelectList(_context.Propuestas, "Id", "Id", historialPropuesta.PropuestaId);
            return View(historialPropuesta);
        }

        // GET: HistorialPropuesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPropuesta = await _context.HistorialPropuestas
                .Include(h => h.Propuesta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historialPropuesta == null)
            {
                return NotFound();
            }

            return View(historialPropuesta);
        }

        // POST: HistorialPropuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialPropuesta = await _context.HistorialPropuestas.FindAsync(id);
            if (historialPropuesta != null)
            {
                _context.HistorialPropuestas.Remove(historialPropuesta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialPropuestaExists(int id)
        {
            return _context.HistorialPropuestas.Any(e => e.Id == id);
        }
    }
}
