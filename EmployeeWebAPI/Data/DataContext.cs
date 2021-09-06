using Microsoft.EntityFrameworkCore;
using EmployeeWebAPIProject.Models;

namespace EmployeeWebAPIProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ignore ForeginKeys properties
            modelBuilder.Entity<Employee>().Ignore(e => e.SubordinateIds);
            modelBuilder.Entity<Employee>().Ignore(e => e.JobCategories);
            modelBuilder.Entity<Employee>().Ignore(e => e.SalaryIds);

            // one to one relations of Address
            modelBuilder.Entity<Address>()
                .HasOne(a => a.City)
                .WithOne(c => c.Address)
                .HasForeignKey<Address>("CityId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Country)
                .WithOne(c => c.Address)
                .HasForeignKey<Address>("CountryId")
                .OnDelete(DeleteBehavior.Restrict);

            // one to one relations of City
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithOne(c => c.City)
                .HasForeignKey<City>("CountryId")
                .OnDelete(DeleteBehavior.Restrict);

            // one to one relations of Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithOne(a => a.Employee)
                .HasForeignKey<Employee>("AddressId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Country)
                .WithOne(a => a.Employee)
                .HasForeignKey<Employee>("CountryId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Superior)
                .WithOne()
                .HasForeignKey<Employee>("SuperiorId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // one to many relations of Emplyee
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.JobCategories1)
                .WithOne(j => j.Employee);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Salaries)
                .WithOne(j => j.Employee)
                .HasForeignKey("SalaryIds")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Subordinates)
                .WithOne()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = default,
                    MiddleName = default,
                    LastName = default,
                    BirthDate = default,
                    Gender = default,
                    AddressId = null,
                    Email = default,
                    PhoneNumber = default,
                    CountryId = null,
                    JoinedDate = default,
                    ExitedDate = default,
                    SalaryIds = null,
                    SuperiorId = null,
                    SubordinateIds = null,
                }
            );
        }
    }
}