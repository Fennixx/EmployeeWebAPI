using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Repositories
{
    public interface ICityRepository
    {
        Task<ServiceResponse<IEnumerable<GetCityDto>>> Create(CreateCityDto newModel);

        Task<ServiceResponse<IEnumerable<GetCityDto>>> Delete(int id);

        Task<ServiceResponse<IEnumerable<GetCityDto>>> GetAll();

        Task<ServiceResponse<GetCityDto>> GetById(int id);

        Task<ServiceResponse<GetCityDto>> Update(UpdateCityDto updatedModel);
    }
}