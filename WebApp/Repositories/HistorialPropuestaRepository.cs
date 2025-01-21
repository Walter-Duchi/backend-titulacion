using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace ProyectoDAWA.Repositories
{
    public class HistorialPropuestaRepository : IHistorialPropuestaRepository
    {
        private readonly ProyectoDAWAContext _context;

        public HistorialPropuestaRepository(ProyectoDAWAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistorialPropuesta>> GetAllAsync()
        {
            return await _context.HistorialPropuestas.ToListAsync();
        }

        public async Task<HistorialPropuesta> GetByIdAsync(int id)
        {
            return await _context.HistorialPropuestas.FindAsync(id) ?? throw new InvalidOperationException("Historial Propuesta not found");
        }

        public async Task AddAsync(HistorialPropuesta historialPropuesta)
        {
            await _context.HistorialPropuestas.AddAsync(historialPropuesta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HistorialPropuesta historialPropuesta)
        {
            _context.HistorialPropuestas.Update(historialPropuesta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var historialPropuesta = await _context.HistorialPropuestas.FindAsync(id);
            if (historialPropuesta != null)
            {
                _context.HistorialPropuestas.Remove(historialPropuesta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
