using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace ProyectoDAWA.Repositories
{
    public class EstudiantesPropuestaRepository : IEstudiantesPropuestaRepository
    {
        private readonly ProyectoDAWAContext _context;

        public EstudiantesPropuestaRepository(ProyectoDAWAContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EstudiantesPropuesta>> GetAllAsync()
        {
            return await _context.EstudiantesPropuestas.ToListAsync();
        }

        public async Task<EstudiantesPropuesta> GetByIdAsync(int id)
        {
            return await _context.EstudiantesPropuestas.FindAsync(id) ?? throw new InvalidOperationException("Estudiante Propuesta not found");
        }

        public async Task AddAsync(EstudiantesPropuesta estudiantePropuesta)
        {
            await _context.EstudiantesPropuestas.AddAsync(estudiantePropuesta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EstudiantesPropuesta estudiantePropuesta)
        {
            _context.EstudiantesPropuestas.Update(estudiantePropuesta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiantePropuesta = await _context.EstudiantesPropuestas.FindAsync(id);
            if (estudiantePropuesta != null)
            {
                _context.EstudiantesPropuestas.Remove(estudiantePropuesta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
