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
    public class CityRepository : ICityRepository
    {
        protected readonly IMapper _mapper;
        private readonly DataContext _context;

        public CityRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<GetCityDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetCityDto>>();
            try
            {
                var dbContent = await _context.Cities.ToListAsync();
                serviceResponse.Data = dbContent.Select(e => _mapper.Map<GetCityDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCityDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCityDto>();
            try
            {
                var dbContent = await _context.Cities.FirstOrDefaultAsync(e => e.Id == id);
                serviceResponse.Data = _mapper.Map<GetCityDto>(dbContent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetCityDto>>> Create(CreateCityDto newCity)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetCityDto>>();
            try
            {
                City city = _mapper.Map<City>(newCity);
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Cities.Select(e => _mapper.Map<GetCityDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCityDto>> Update(UpdateCityDto updatedCity)
        {
            var serviceResponse = new ServiceResponse<GetCityDto>();
            try
            {
                City city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == updatedCity.Id);

                _mapper.Map(updatedCity, city);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCityDto>(city);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetCityDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetCityDto>>();
            try
            {
                City city = await _context.Cities.FirstAsync(e => e.Id == id);
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Cities.Select(e => _mapper.Map<GetCityDto>(e)).ToList();
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