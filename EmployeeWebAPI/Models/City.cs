using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebAPI.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }

        public Country Country { get; set; }
        public Address Address { get; set; }
    }
}