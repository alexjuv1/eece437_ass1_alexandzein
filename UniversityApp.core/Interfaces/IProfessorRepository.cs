using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.Core.Entities;

public interface IProfessorRepository
    {
        Task<Professor> GetByIdAsync(int professorId);
        Task<IReadOnlyList<Professor>> GetAllAsync();
        Task AddAsync(Professor professor);
        Task UpdateAsync(Professor professor);
        Task DeleteAsync(Professor professor);
    }