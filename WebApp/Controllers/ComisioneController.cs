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
    public class ComisioneController : Controller
    {
        private readonly ProyectoDAWAContext _context;

        public ComisioneController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: Comisione
        public async Task<IActionResult> Index()
        {
            var proyectoDAWAContext = _context.Comisiones.Include(c => c.Gestor);
            return View(await proyectoDAWAContext.ToListAsync());
        }

        // GET: Comisione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comisione = await _context.Comisiones
                .Include(c => c.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comisione == null)
            {
                return NotFound();
            }

            return View(comisione);
        }

        // GET: Comisione/Create
        public IActionResult Create()
        {
            ViewData["GestorId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Comisione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Proposito,Estado,FechaCreación,GestorId")] Comisione comisione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comisione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GestorId"] = new SelectList(_context.Usuarios, "Id", "Id", comisione.GestorId);
            return View(comisione);
        }

        // GET: Comisione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comisione = await _context.Comisiones.FindAsync(id);
            if (comisione == null)
            {
                return NotFound();
            }
            ViewData["GestorId"] = new SelectList(_context.Usuarios, "Id", "Id", comisione.GestorId);
            return View(comisione);
        }

        // POST: Comisione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Proposito,Estado,FechaCreación,GestorId")] Comisione comisione)
        {
            if (id != comisione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comisione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComisioneExists(comisione.Id))
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
            ViewData["GestorId"] = new SelectList(_context.Usuarios, "Id", "Id", comisione.GestorId);
            return View(comisione);
        }

        // GET: Comisione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comisione = await _context.Comisiones
                .Include(c => c.Gestor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comisione == null)
            {
                return NotFound();
            }

            return View(comisione);
        }

        // POST: Comisione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comisione = await _context.Comisiones.FindAsync(id);
            if (comisione != null)
            {
                _context.Comisiones.Remove(comisione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComisioneExists(int id)
        {
            return _context.Comisiones.Any(e => e.Id == id);
        }
    }
}
