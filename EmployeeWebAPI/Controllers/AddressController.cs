using Microsoft.AspNetCore.Mvc;
using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAddressDto>>>> GetAll()
        {
            var response = await _addressRepository.GetAll();
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetAddressDto>>> GetOne(int id)
        {
            var response = await _addressRepository.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAddressDto>>>> CreateAddress([FromBody] CreateAddressDto newAddress)
        {
            if (newAddress == null) return BadRequest();

            return Ok(await _addressRepository.Create(newAddress));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetAddressDto>>> UpdateAddress(UpdateAddressDto updatedAddress)
        {
            if (updatedAddress == null) return BadRequest();

            var response = await _addressRepository.Update(updatedAddress);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<GetAddressDto>>>> DeleteOne(int id)
        {
            var response = await _addressRepository.Delete(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}