using EmployeeWebAPI.Models;
using System;
using System.Collections.Generic;

namespace EmployeeWebAPI.Dtos
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int AddressId { get; set; }
        public IEnumerable<int> JobCategories { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public DateTimeOffset JoinedDate { get; set; }
        public DateTimeOffset ExitedDate { get; set; }
        public IEnumerable<decimal> SalaryIds { get; set; }
        public int SuperiorId { get; set; }
        public IEnumerable<int> SubordinateIds { get; set; }
    }
}