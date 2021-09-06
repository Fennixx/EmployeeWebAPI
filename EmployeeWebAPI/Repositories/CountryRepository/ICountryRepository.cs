using EmployeeWebAPIProject.Dtos;
using EmployeeWebAPIProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPIProject.Repositories
{
    public interface ICountryRepository
    {
        Task<ServiceResponse<IEnumerable<GetCountryDto>>> Create(CreateCountryDto newModel);

        Task<ServiceResponse<IEnumerable<GetCountryDto>>> Delete(int id);

        Task<ServiceResponse<IEnumerable<GetCountryDto>>> GetAll();

        Task<ServiceResponse<GetCountryDto>> GetById(int id);

        Task<ServiceResponse<GetCountryDto>> Update(UpdateCountryDto updatedModel);
    }
}