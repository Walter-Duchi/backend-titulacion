using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace ProyectoDAWA.Repositories
{
    public class PeriodoRepository : IPeriodoRepository
    {
        private readonly ProyectoDAWAContext _context;

        public PeriodoRepository(ProyectoDAWAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Periodo>> GetAllAsync()
        {
            return await _context.Periodos.ToListAsync();
        }

        public async Task<Periodo> GetByIdAsync(int id)
        {
            return await _context.Periodos.FindAsync(id) ?? throw new InvalidOperationException("Periodo not found");
        }

        public async Task AddAsync(Periodo periodo)
        {
            await _context.Periodos.AddAsync(periodo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Periodo periodo)
        {
            _context.Periodos.Update(periodo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo != null)
            {
                _context.Periodos.Remove(periodo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
