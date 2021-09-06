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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCityDto>>>> GetAll()
        {
            var response = await _cityRepository.GetAll();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCityDto>>> GetOne(int id)
        {
            var response = await _cityRepository.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCityDto>>>> CreateCity([FromBody] CreateCityDto newCity)
        {
            if (newCity == null) return BadRequest();

            return Ok(await _cityRepository.Create(newCity));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCityDto>>> UpdateCity(UpdateCityDto updatedCity)
        {
            if (updatedCity == null) return BadRequest();

            var response = await _cityRepository.Update(updatedCity);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetCityDto>>>> DeleteOne(int id)
        {
            var response = await _cityRepository.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}