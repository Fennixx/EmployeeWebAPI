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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetEmployeeDto>>>> GetAll()
        {
            var response = await _employeeRepository.GetAll();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> GetOne(int id)
        {
            var response = await _employeeRepository.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetEmployeeDto>>>> CreateEmployee([FromBody] CreateEmployeeDto newEmloyee)
        {
            if (newEmloyee == null) return BadRequest();

            return Ok(await _employeeRepository.Create(newEmloyee));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            if (updatedEmployee == null) return BadRequest();

            var response = await _employeeRepository.Update(updatedEmployee);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetEmployeeDto>>>> DeleteOne(int id)
        {
            var response = await _employeeRepository.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}