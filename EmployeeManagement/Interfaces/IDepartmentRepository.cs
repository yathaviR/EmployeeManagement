using EmployeeManagement.Models;
namespace EmployeeManagement.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
