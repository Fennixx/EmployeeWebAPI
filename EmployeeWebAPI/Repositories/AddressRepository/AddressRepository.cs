using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EmployeeWebAPI.Data;
using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        protected readonly IMapper _mapper;
        private readonly DataContext _context;

        public AddressRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<GetAddressDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetAddressDto>>();
            try
            {
                var dbContent = await _context.Addresses.ToListAsync();
                serviceResponse.Data = dbContent.Select(e => _mapper.Map<GetAddressDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetAddressDto>();
            try
            {
                var dbContent = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == id);
                serviceResponse.Data = _mapper.Map<GetAddressDto>(dbContent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetAddressDto>>> Create(CreateAddressDto newAddress)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetAddressDto>>();
            try
            {
                Address address = _mapper.Map<Address>(newAddress);
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Addresses.Select(e => _mapper.Map<GetAddressDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAddressDto>> Update(UpdateAddressDto updatedAddress)
        {
            var serviceResponse = new ServiceResponse<GetAddressDto>();
            try
            {
                Address address = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == updatedAddress.Id);

                _mapper.Map(updatedAddress, address);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetAddressDto>(address);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetAddressDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetAddressDto>>();
            try
            {
                Address address = await _context.Addresses.FirstAsync(e => e.Id == id);
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Addresses.Select(e => _mapper.Map<GetAddressDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}