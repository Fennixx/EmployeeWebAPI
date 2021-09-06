using EmployeeWebAPIProject.Models;

namespace EmployeeWebAPIProject.Dtos
{
    public class GetCityDto : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }
}