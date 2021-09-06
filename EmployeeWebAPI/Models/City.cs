using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebAPIProject.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }

        public Country Country { get; set; }
        public Address Address { get; set; }
    }
}