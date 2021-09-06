using EmployeeWebAPIProject.Models;

namespace EmployeeWebAPIProject.Dtos
{
    public class UpdateCityDto : BaseEntity
    {
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }
}