using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using UniversityApp.Infrastructure.Persistence;

namespace UniversityApp.Infrastructure.Persistence.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;
        public ProfessorRepository(AppDbContext context) => _context = context;

        public async Task<Professor> GetByIdAsync(int professorId)
        {
            return await _context.Professors
                .Include(p => p.Courses)
                .FirstOrDefaultAsync(p => p.ProfessorId == professorId);
        }

        public async Task<IReadOnlyList<Professor>> GetAllAsync()
        {
            return await _context.Professors
                .Include(p => p.Courses)
                .ToListAsync();
        }

        public async Task AddAsync(Professor professor)
        {
            await _context.Professors.AddAsync(professor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Professor professor)
        {
            _context.Professors.Update(professor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Professor professor)
        {
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
