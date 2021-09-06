using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Dtos
{
    public class GetCityDto : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }
}