using AutoMapper;
using EmployeeWebAPIProject.Dtos;
using EmployeeWebAPIProject.Models;

namespace EmployeeWebAPIProject
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Emloyee
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<GetEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, CreateEmployeeDto>();

            // Address
            CreateMap<Address, GetAddressDto>();
            CreateMap<GetAddressDto, Address>();
            CreateMap<Address, UpdateAddressDto>();
            CreateMap<UpdateAddressDto, Address>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<Address, CreateAddressDto>();

            // City
            CreateMap<City, GetCityDto>();
            CreateMap<GetCityDto, City>();
            CreateMap<City, UpdateCityDto>();
            CreateMap<UpdateCityDto, City>();
            CreateMap<CreateCityDto, City>();
            CreateMap<City, CreateCityDto>();

            // Country
            CreateMap<Country, GetCountryDto>();
            CreateMap<GetCountryDto, Country>();
            CreateMap<Country, UpdateCountryDto>();
            CreateMap<UpdateCountryDto, Country>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<Country, CreateCountryDto>();

            // JobCategory
            CreateMap<JobCategory, GetJobCategoryDto>();
            CreateMap<GetJobCategoryDto, JobCategory>();
            CreateMap<JobCategory, UpdateJobCategoryDto>();
            CreateMap<UpdateJobCategoryDto, JobCategory>();
            CreateMap<CreateJobCategoryDto, JobCategory>();
            CreateMap<JobCategory, CreateJobCategoryDto>();

            // Salary
            CreateMap<Salary, GetSalaryDto>();
            CreateMap<GetSalaryDto, Salary>();
            CreateMap<Salary, UpdateSalaryDto>();
            CreateMap<UpdateSalaryDto, Salary>();
            CreateMap<CreateSalaryDto, Salary>();
            CreateMap<Salary, CreateSalaryDto>();
        }
    }
}