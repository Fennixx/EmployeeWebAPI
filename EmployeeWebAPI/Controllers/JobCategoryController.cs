using Microsoft.AspNetCore.Mvc;
using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobCategoryController : ControllerBase
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public JobCategoryController(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetJobCategoryDto>>>> GetAll()
        {
            var response = await _jobCategoryRepository.GetAll();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetJobCategoryDto>>> GetOne(int id)
        {
            var response = await _jobCategoryRepository.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetJobCategoryDto>>>> CreateJobCategory([FromBody] CreateJobCategoryDto newJobCategory)
        {
            if (newJobCategory == null) return BadRequest();

            return Ok(await _jobCategoryRepository.Create(newJobCategory));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetJobCategoryDto>>> UpdateJobCategory(UpdateJobCategoryDto updatedJobCategory)
        {
            if (updatedJobCategory == null) return BadRequest();

            var response = await _jobCategoryRepository.Update(updatedJobCategory);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetJobCategoryDto>>>> DeleteOne(int id)
        {
            var response = await _jobCategoryRepository.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}