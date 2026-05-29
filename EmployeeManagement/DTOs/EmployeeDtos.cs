namespace EmployeeManagement.DTOs;

public record CreateEmployeeDto
(
    string Name,
    string Email,
    DateTime Dob,
    string Role,
    decimal Salary,
    int DepartmentId,
    int? ManagerId
);  

public record UpdateEmployeeDto(
    string Name,
    string Email,
    DateTime Dob,
    string Role,
    decimal Salary,
    int DepartmentId,
    int? ManagerId
);