using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.Core.Entities;

public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(int courseId);
        Task<IReadOnlyList<Course>> GetAllAsync();
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Course course);
    }