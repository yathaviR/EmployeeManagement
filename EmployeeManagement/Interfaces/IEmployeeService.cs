using EmployeeManagement.Models;
using EmployeeManagement.DTOs;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<(bool Success, string Error)> CreateAsync(CreateEmployeeDto dto);
        Task<(bool Success, string Error)> UpdateAsync(int id, UpdateEmployeeDto dto);
        Task<(bool Success, string Error)> DeleteAsync(int id);
    }
}
