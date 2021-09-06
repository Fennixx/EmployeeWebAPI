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
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;

        public SalaryController(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetSalaryDto>>>> GetAll()
        {
            var response = await _salaryRepository.GetAll();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSalaryDto>>> GetOne(int id)
        {
            var response = await _salaryRepository.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetSalaryDto>>>> CreateSalary([FromBody] CreateSalaryDto newSalary)
        {
            if (newSalary == null) return BadRequest();

            return Ok(await _salaryRepository.Create(newSalary));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetSalaryDto>>> UpdateSalary(UpdateSalaryDto updatedSalary)
        {
            if (updatedSalary == null) return BadRequest();

            var response = await _salaryRepository.Update(updatedSalary);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetSalaryDto>>>> DeleteOne(int id)
        {
            var response = await _salaryRepository.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}