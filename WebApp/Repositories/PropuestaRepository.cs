using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace ProyectoDAWA.Repositories
{
    public class PropuestaRepository : IPropuestaRepository
    {
        private readonly ProyectoDAWAContext _context;

        public PropuestaRepository(ProyectoDAWAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Propuesta>> GetAllAsync()
        {
            return await _context.Propuestas.ToListAsync();
        }

        public async Task<Propuesta> GetByIdAsync(int id)
        {
            return await _context.Propuestas.FindAsync(id) ?? throw new InvalidOperationException("Propuesta not found");
        }

        public async Task AddAsync(Propuesta propuesta)
        {
            await _context.Propuestas.AddAsync(propuesta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Propuesta propuesta)
        {
            _context.Propuestas.Update(propuesta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var propuesta = await _context.Propuestas.FindAsync(id);
            if (propuesta != null)
            {
                _context.Propuestas.Remove(propuesta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
