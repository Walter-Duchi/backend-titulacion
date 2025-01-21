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
    public class MiembrosComisionController : Controller
    {
        private readonly ProyectoDAWAContext _context;

        public MiembrosComisionController(ProyectoDAWAContext context)
        {
            _context = context;
        }

        // GET: MiembrosComision
        public async Task<IActionResult> Index()
        {
            var proyectoDAWAContext = _context.MiembrosComisions.Include(m => m.CoordinadorComision).Include(m => m.MiembrosComisionNavigation);
            return View(await proyectoDAWAContext.ToListAsync());
        }

        // GET: MiembrosComision/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembrosComision = await _context.MiembrosComisions
                .Include(m => m.CoordinadorComision)
                .Include(m => m.MiembrosComisionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miembrosComision == null)
            {
                return NotFound();
            }

            return View(miembrosComision);
        }

        // GET: MiembrosComision/Create
        public IActionResult Create()
        {
            ViewData["CoordinadorComisionId"] = new SelectList(_context.MiembrosComisions, "Id", "Id");
            ViewData["MiembrosComisionId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: MiembrosComision/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MiembrosComisionId,CoordinadorComisionId")] MiembrosComision miembrosComision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miembrosComision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinadorComisionId"] = new SelectList(_context.MiembrosComisions, "Id", "Id", miembrosComision.CoordinadorComisionId);
            ViewData["MiembrosComisionId"] = new SelectList(_context.Usuarios, "Id", "Id", miembrosComision.MiembrosComisionId);
            return View(miembrosComision);
        }

        // GET: MiembrosComision/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembrosComision = await _context.MiembrosComisions.FindAsync(id);
            if (miembrosComision == null)
            {
                return NotFound();
            }
            ViewData["CoordinadorComisionId"] = new SelectList(_context.MiembrosComisions, "Id", "Id", miembrosComision.CoordinadorComisionId);
            ViewData["MiembrosComisionId"] = new SelectList(_context.Usuarios, "Id", "Id", miembrosComision.MiembrosComisionId);
            return View(miembrosComision);
        }

        // POST: MiembrosComision/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MiembrosComisionId,CoordinadorComisionId")] MiembrosComision miembrosComision)
        {
            if (id != miembrosComision.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miembrosComision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembrosComisionExists(miembrosComision.Id))
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
            ViewData["CoordinadorComisionId"] = new SelectList(_context.MiembrosComisions, "Id", "Id", miembrosComision.CoordinadorComisionId);
            ViewData["MiembrosComisionId"] = new SelectList(_context.Usuarios, "Id", "Id", miembrosComision.MiembrosComisionId);
            return View(miembrosComision);
        }

        // GET: MiembrosComision/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miembrosComision = await _context.MiembrosComisions
                .Include(m => m.CoordinadorComision)
                .Include(m => m.MiembrosComisionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (miembrosComision == null)
            {
                return NotFound();
            }

            return View(miembrosComision);
        }

        // POST: MiembrosComision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miembrosComision = await _context.MiembrosComisions.FindAsync(id);
            if (miembrosComision != null)
            {
                _context.MiembrosComisions.Remove(miembrosComision);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiembrosComisionExists(int id)
        {
            return _context.MiembrosComisions.Any(e => e.Id == id);
        }
    }
}
