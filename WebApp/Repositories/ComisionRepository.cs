using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace ProyectoDAWA.Repositories
{
    public class ComisionRepository : IComisionRepository
    {
        private readonly ProyectoDAWAContext _context;

        public ComisionRepository(ProyectoDAWAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comisione>> GetAllAsync()
        {
            return await _context.Comisiones.ToListAsync();
        }

        public async Task<Comisione> GetByIdAsync(int id)
        {
            return await _context.Comisiones.FindAsync(id) ?? throw new InvalidOperationException("Comisión not found");
        }

        public async Task AddAsync(Comisione comision)
        {
            await _context.Comisiones.AddAsync(comision);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comisione comision)
        {
            _context.Comisiones.Update(comision);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comision = await _context.Comisiones.FindAsync(id);
            if (comision != null)
            {
                _context.Comisiones.Remove(comision);
                await _context.SaveChangesAsync();
            }
        }
    }
}
