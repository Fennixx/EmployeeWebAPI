using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Repositories
{
    public interface IAddressRepository
    {
        Task<ServiceResponse<IEnumerable<GetAddressDto>>> Create(CreateAddressDto newModel);

        Task<ServiceResponse<IEnumerable<GetAddressDto>>> Delete(int id);

        Task<ServiceResponse<IEnumerable<GetAddressDto>>> GetAll();

        Task<ServiceResponse<GetAddressDto>> GetById(int id);

        Task<ServiceResponse<GetAddressDto>> Update(UpdateAddressDto updatedModel);
    }
}