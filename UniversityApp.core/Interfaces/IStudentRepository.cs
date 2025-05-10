using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.Core.Entities;

namespace UniversityApp.Core.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(int studentId);
        Task<IReadOnlyList<Student>> GetAllAsync();
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
    }
}
