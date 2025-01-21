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
    public class PropuestaController : Controller
    {
        private readonly ProyectoDAWAContext _context;

        public PropuestaController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: Propuesta
        public async Task<IActionResult> Index()
        {
            var proyectoDAWAContext = _context.Propuestas.Include(p => p.Revisor);
            return View(await proyectoDAWAContext.ToListAsync());
        }

        // GET: Propuesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = await _context.Propuestas
                .Include(p => p.Revisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propuesta == null)
            {
                return NotFound();
            }

            return View(propuesta);
        }

        // GET: Propuesta/Create
        public IActionResult Create()
        {
            ViewData["RevisorId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Propuesta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,FechaCreación,Calificacion,RevisorId")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RevisorId"] = new SelectList(_context.Usuarios, "Id", "Id", propuesta.RevisorId);
            return View(propuesta);
        }

        // GET: Propuesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = await _context.Propuestas.FindAsync(id);
            if (propuesta == null)
            {
                return NotFound();
            }
            ViewData["RevisorId"] = new SelectList(_context.Usuarios, "Id", "Id", propuesta.RevisorId);
            return View(propuesta);
        }

        // POST: Propuesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaCreación,Calificacion,RevisorId")] Propuesta propuesta)
        {
            if (id != propuesta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propuesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropuestaExists(propuesta.Id))
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
            ViewData["RevisorId"] = new SelectList(_context.Usuarios, "Id", "Id", propuesta.RevisorId);
            return View(propuesta);
        }

        // GET: Propuesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propuesta = await _context.Propuestas
                .Include(p => p.Revisor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propuesta == null)
            {
                return NotFound();
            }

            return View(propuesta);
        }

        // POST: Propuesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propuesta = await _context.Propuestas.FindAsync(id);
            if (propuesta != null)
            {
                _context.Propuestas.Remove(propuesta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropuestaExists(int id)
        {
            return _context.Propuestas.Any(e => e.Id == id);
        }
    }
}
