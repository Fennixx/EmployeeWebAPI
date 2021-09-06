namespace EmployeeWebAPI.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public Address Address { get; set; }
        public City City { get; set; }
        public Employee Employee { get; set; }
    }
}