using EmployeeManagement.Models;
namespace EmployeeManagement.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
    }
}
