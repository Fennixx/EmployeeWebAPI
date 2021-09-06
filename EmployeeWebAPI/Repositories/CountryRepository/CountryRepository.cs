using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EmployeeWebAPIProject.Data;
using EmployeeWebAPIProject.Dtos;
using EmployeeWebAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPIProject.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        protected readonly IMapper _mapper;
        private readonly DataContext _context;

        public CountryRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<GetCountryDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetCountryDto>>();
            try
            {
                var dbContent = await _context.Countries.ToListAsync();
                serviceResponse.Data = dbContent.Select(e => _mapper.Map<GetCountryDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCountryDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCountryDto>();
            try
            {
                var dbContent = await _context.Countries.FirstOrDefaultAsync(e => e.Id == id);
                serviceResponse.Data = _mapper.Map<GetCountryDto>(dbContent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetCountryDto>>> Create(CreateCountryDto newCountry)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetCountryDto>>();
            try
            {
                Country country = _mapper.Map<Country>(newCountry);
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Countries.Select(e => _mapper.Map<GetCountryDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCountryDto>> Update(UpdateCountryDto updatedCountry)
        {
            var serviceResponse = new ServiceResponse<GetCountryDto>();
            try
            {
                Country country = await _context.Countries.FirstOrDefaultAsync(e => e.Id == updatedCountry.Id);

                _mapper.Map(updatedCountry, country);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCountryDto>(country);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetCountryDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetCountryDto>>();
            try
            {
                Country country = await _context.Countries.FirstAsync(e => e.Id == id);
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Countries.Select(e => _mapper.Map<GetCountryDto>(e)).ToList();
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