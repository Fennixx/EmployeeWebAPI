using System;
using System.Collections.Generic;

namespace EmployeeWebAPI.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int? AddressId { get; set; }
        public IEnumerable<int> JobCategories { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? CountryId { get; set; }
        public DateTimeOffset JoinedDate { get; set; }
        public DateTimeOffset ExitedDate { get; set; }
        public IEnumerable<decimal> SalaryIds { get; set; }
        public int? SuperiorId { get; set; }
        public IEnumerable<int> SubordinateIds { get; set; }

        public Address Address { get; set; }
        public Country Country { get; set; }
        public Employee Superior { get; set; }
        public IEnumerable<Salary> Salaries { get; set; }
        public IEnumerable<JobCategory> JobCategories1 { get; set; }
        public IEnumerable<Employee> Subordinates { get; set; }
    }
}