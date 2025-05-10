using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.Core.Entities;

public interface IDepartmentRepository
    {
        Task<Department> GetByIdAsync(int departmentId);
        Task<IReadOnlyList<Department>> GetAllAsync();
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);
    }