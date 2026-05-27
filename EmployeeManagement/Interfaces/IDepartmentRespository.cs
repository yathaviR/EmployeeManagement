using EmployeeManagement.Models;
namespace EmployeeManagement.Interfaces
{
    public interface IDepartmentRespository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
