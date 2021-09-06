using EmployeeWebAPIProject.Dtos;
using EmployeeWebAPIProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPIProject.Repositories
{
    public interface IEmployeeRepository
    {
        Task<ServiceResponse<IEnumerable<GetEmployeeDto>>> Create(CreateEmployeeDto newModel);

        Task<ServiceResponse<IEnumerable<GetEmployeeDto>>> Delete(int id);

        Task<ServiceResponse<IEnumerable<GetEmployeeDto>>> GetAll();

        Task<ServiceResponse<GetEmployeeDto>> GetById(int id);

        Task<ServiceResponse<GetEmployeeDto>> Update(UpdateEmployeeDto updatedModel);
    }
}