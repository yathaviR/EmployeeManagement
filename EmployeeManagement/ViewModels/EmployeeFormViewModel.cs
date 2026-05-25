using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeFormViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required")] 
        public DateOnly Dob { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, 9999999, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }
        
        [Required(ErrorMessage ="Role is required")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        public int? DepartmentId { get; set; } 

        public int? ManagerId { get; set; }

       public List<SelectListItem> Departments { get; set; } = new();
       public List<SelectListItem> Managers { get; set; } = new();
    }
}
