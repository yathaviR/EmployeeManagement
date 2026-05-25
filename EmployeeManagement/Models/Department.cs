namespace EmployeeManagement.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Employee> Employees { get; set; }= new List<Employee>();

        
    }
}
