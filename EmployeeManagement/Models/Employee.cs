namespace EmployeeManagement.Models
{
    /// <summary>
    /// Represents an employee
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeID { get; set; }

        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }

        public decimal Salary { get; set; }
        public string Role { get; set; }
        public int DepartmentID { get; set; }
        public int? ManagerID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Department Department { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
        public Employee Manager { get; set; }

    }
}
