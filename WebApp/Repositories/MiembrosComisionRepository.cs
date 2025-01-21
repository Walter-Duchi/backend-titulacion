using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace ProyectoDAWA.Repositories
{
    public class MiembrosComisionRepository : IMiembrosComisionRepository
    {
        private readonly ProyectoDAWAContext _context;

        public MiembrosComisionRepository(ProyectoDAWAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MiembrosComision>> GetAllAsync()
        {
            return await _context.MiembrosComisions.ToListAsync();
        }

        public async Task<MiembrosComision> GetByIdAsync(int id)
        {
            return await _context.MiembrosComisions.FindAsync(id) ?? throw new InvalidOperationException("Miembro Comisión not found");
        }

        public async Task AddAsync(MiembrosComision miembrosComision)
        {
            await _context.MiembrosComisions.AddAsync(miembrosComision);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MiembrosComision miembrosComision)
        {
            _context.MiembrosComisions.Update(miembrosComision);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var miembrosComision = await _context.MiembrosComisions.FindAsync(id);
            if (miembrosComision != null)
            {
                _context.MiembrosComisions.Remove(miembrosComision);
                await _context.SaveChangesAsync();
            }
        }
    }
}
