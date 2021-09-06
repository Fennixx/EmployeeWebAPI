using EmployeeWebAPI.Models;

namespace EmployeeWebAPI.Dtos
{
    public class UpdateJobCategoryDto : BaseEntity
    {
        public string Title { get; set; }
    }
}