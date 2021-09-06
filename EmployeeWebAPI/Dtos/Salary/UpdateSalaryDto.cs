using EmployeeWebAPIProject.Models;
using System;

namespace EmployeeWebAPIProject.Dtos
{
    public class UpdateSalaryDto : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset? To { get; set; } = null;
    }
}