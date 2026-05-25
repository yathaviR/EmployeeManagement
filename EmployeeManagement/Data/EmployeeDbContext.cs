using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
//using Microsoft.SqlServer;

namespace EmployeeManagement.Data
{
    public class EmployeeDbContext : DbContext
    {
        //Constructor

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        // DB Sets

        public DbSet<Models.Employee> Employees { get; set; }
        public DbSet<Models.Department> Departments { get; set; }

        // OnModelCreating(Column rules, indexes, relationships)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeID)
                .IsRequired()
                .HasColumnType("varchar(8)")
                .HasMaxLength(8);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.Dob)
                .IsRequired();

                entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.DepartmentID)
                .IsRequired();

                entity.Property(e => e.ManagerID)
                .IsRequired(false);

                entity.Property(e => e.Salary)
                .IsRequired()
                // .HasPrecision(18,2);
                 .HasColumnType("decimal(18,2)");

                entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                .IsRequired(false);

                // Relationships
                entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Manager)
                .WithMany(d => d.Subordinates)
                .HasForeignKey(e => e.ManagerID)
                .OnDelete(DeleteBehavior.Restrict);


                // Indexes
                entity.HasIndex(e => e.EmployeeID).IsUnique();
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Role);
                entity.HasIndex(e => e.DepartmentID);
                entity.HasIndex(e => e.ManagerID);

                // Composite Indexes for common queries
                entity.HasIndex(e=> new { e.DepartmentID, e.Role });
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id)
                .ValueGeneratedOnAdd();

                entity.Property(d => d.DepartmentId)
                .IsRequired()
                .HasColumnType("varchar(8)")
                .HasMaxLength(8);

                entity.Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(d => d.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(d => d.UpdatedAt)
                .IsRequired(false);

                // Indexes
                entity.HasIndex(d => d.DepartmentId).IsUnique();
                entity.HasIndex(d => d.DepartmentName);
            });
        }
    }
}
