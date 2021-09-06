using EmployeeWebAPIProject.Models;
using System;

namespace EmployeeWebAPIProject.Dtos
{
    public class GetSalaryDto : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset? To { get; set; } = null;
    }
}