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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCountryDto>>>> GetAll()
        {
            var response = await _countryRepository.GetAll();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCountryDto>>> GetOne(int id)
        {
            var response = await _countryRepository.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCountryDto>>>> CreateCountry([FromBody] CreateCountryDto newCountry)
        {
            if (newCountry == null) return BadRequest();

            return Ok(await _countryRepository.Create(newCountry));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCountryDto>>> UpdateCountry(UpdateCountryDto updatedCountry)
        {
            if (updatedCountry == null) return BadRequest();

            var response = await _countryRepository.Update(updatedCountry);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCountryDto>>>> DeleteOne(int id)
        {
            var response = await _countryRepository.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}