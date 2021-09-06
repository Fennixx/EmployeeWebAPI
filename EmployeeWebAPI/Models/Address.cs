using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWebAPI.Models
{
    public class Address : BaseEntity
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string ZipCode { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }

        public City City { get; set; }
        public Country Country { get; set; }
        public Employee Employee { get; set; }
    }
}