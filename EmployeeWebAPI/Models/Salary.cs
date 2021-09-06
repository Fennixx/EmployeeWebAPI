using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebAPIProject.Models
{
    public class Salary : BaseEntity
    {
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        public DateTimeOffset From { get; set; }
        public DateTimeOffset? To { get; set; } = null;

        public Employee Employee { get; set; }
    }
}