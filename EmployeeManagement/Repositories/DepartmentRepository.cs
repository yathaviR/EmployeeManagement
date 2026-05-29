using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;

namespace EmployeeManagement.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeDbContext _context;

        public DepartmentRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking() // read-only, improves performance
                .OrderBy(d => d.DepartmentName) // alphabetical order for departments dropdown
                .ToListAsync();
        }
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }
    }
}
