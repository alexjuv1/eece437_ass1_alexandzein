using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using UniversityApp.Infrastructure.Persistence;

namespace UniversityApp.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) => _context = context;

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            return await _context.Departments
                .Include(d => d.Professors)
                .Include(d => d.Courses)
                .Include(d => d.Students)
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<IReadOnlyList<Department>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.Professors)
                .Include(d => d.Courses)
                .Include(d => d.Students)
                .ToListAsync();
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}
