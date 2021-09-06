using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Repositories
{
    public interface IJobCategoryRepository
    {
        Task<ServiceResponse<IEnumerable<GetJobCategoryDto>>> Create(CreateJobCategoryDto newModel);

        Task<ServiceResponse<IEnumerable<GetJobCategoryDto>>> Delete(int id);

        Task<ServiceResponse<IEnumerable<GetJobCategoryDto>>> GetAll();

        Task<ServiceResponse<GetJobCategoryDto>> GetById(int id);

        Task<ServiceResponse<GetJobCategoryDto>> Update(UpdateJobCategoryDto updatedModel);
    }
}