using EmployeeManagement.Models;
using EmployeeManagement.Interfaces;
using EmployeeManagement.DTOs;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRespository _departmentRepository;
            
        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRespository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string Error)> CreateAsync(CreateEmployeeDto dto)
        {
            // Rule 1  :Validate department exists
            var departmentExists = await _departmentRepository.ExistsAsync(dto.DepartmentId);
            if (!departmentExists)
                return (false, "Selected Department does not exist.");

            // Rule 2 : email must be unique
            var existingEmployee = await _employeeRepository.GetByEmailAsync(dto.Email);
            if (existingEmployee is not null)
                return (false, "An employee with the same email already exists."); 
            
            // Map DTO to entity
            var employee = new Employee
            {
                Name = dto.Name.Trim(),
                Email = dto.Email.Trim().ToLower(),
                Dob = dto.Dob,
                Role = dto.Role.Trim(),
                Salary = dto.Salary,
                DepartmentID = dto.DepartmentId,
                ManagerID = dto.ManagerId,
                CreatedAt = DateTime.UtcNow
            };

            // Save - get the generated ID back
            var created = await _employeeRepository.CreateAsync(employee);

            // Generated EmployeeID format: EMP{GeneratedID}
            created.EmployeeID = $"EMP{created.Id:D4}"; // EMP0001, EMP0002, etc.

            await _employeeRepository.UpdateAsync(created);

            return (true, string.Empty);
        }


        public async Task<(bool Success, string Error)> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return (false, "Employee not found.");
            // Validate department exists
            if (!await _departmentRepository.ExistsAsync(dto.DepartmentId))
                return (false, "Department does not exist.");
            // Validate manager exists if provided
            if (dto.ManagerId.HasValue && !await _employeeRepository.ExistsAsync(dto.ManagerId.Value))
                return (false, "Manager does not exist.");
            existingEmployee.Name = dto.Name;
            existingEmployee.Email = dto.Email;
            existingEmployee.Dob = dto.Dob;
            existingEmployee.Role = dto.Role;
            existingEmployee.Salary = dto.Salary;
            existingEmployee.DepartmentID = dto.DepartmentId;
            existingEmployee.ManagerID = dto.ManagerId;
            await _employeeRepository.UpdateAsync(existingEmployee);
            return (true, string.Empty);
        }
        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            // Rule 1: Check if employee exists
            var exists = await _employeeRepository.ExistsAsync(id);
            if (!exists)
                return (false, "Employee not found.");

            await _employeeRepository.DeleteAsync(id);
            return (true, string.Empty);
        }
    }
}
