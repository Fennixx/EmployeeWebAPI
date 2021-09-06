namespace EmployeeWebAPI.Models
{
    public class JobCategory : BaseEntity
    {
        public string Title { get; set; }
        public Employee Employee { get; set; }
    }
}