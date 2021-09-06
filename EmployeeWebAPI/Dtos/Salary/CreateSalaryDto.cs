using System;

namespace EmployeeWebAPIProject.Dtos
{
    public class CreateSalaryDto
    {
        public decimal Amount { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset? To { get; set; } = null;
    }
}