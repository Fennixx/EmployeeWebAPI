using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Dtos
{
    public class UpdateCityDto : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }
}