using EmployeeWebAPI.Models;
using System;

namespace EmployeeWebAPI.Dtos
{
    public class GetSalaryDto : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset? To { get; set; } = null;
    }
}