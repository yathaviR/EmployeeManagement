using EmployeeManagement.Models;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }
        public string EmployeeID { get; set; } = string.Empty;
        public string Name { get; set; }= string.Empty;
        public DateOnly Dob { get; set; } 
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Role { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string ManagerName { get; set; }= string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
