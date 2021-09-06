using EmployeeWebAPIProject.Dtos;
using EmployeeWebAPIProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPIProject.Repositories
{
    public interface ISalaryRepository
    {
        Task<ServiceResponse<IEnumerable<GetSalaryDto>>> Create(CreateSalaryDto newModel);

        Task<ServiceResponse<IEnumerable<GetSalaryDto>>> Delete(int id);

        Task<ServiceResponse<IEnumerable<GetSalaryDto>>> GetAll();

        Task<ServiceResponse<GetSalaryDto>> GetById(int id);

        Task<ServiceResponse<GetSalaryDto>> Update(UpdateSalaryDto updatedModel);
    }
}