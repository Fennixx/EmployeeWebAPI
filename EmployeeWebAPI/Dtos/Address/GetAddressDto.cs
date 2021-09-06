using EmployeeWebAPIProject.Models;

namespace EmployeeWebAPIProject.Dtos
{
    public class GetAddressDto : BaseEntity
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string ZipCode { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
    }
}