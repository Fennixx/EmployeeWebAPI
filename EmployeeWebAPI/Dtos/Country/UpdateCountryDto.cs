using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Dtos
{
    public class UpdateCountryDto : BaseEntity
    {
        public string Name { get; set; }
    }
}