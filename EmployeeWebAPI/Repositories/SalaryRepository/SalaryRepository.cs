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
    public class SalaryRepository : ISalaryRepository
    {
        protected readonly IMapper _mapper;
        private readonly DataContext _context;

        public SalaryRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<GetSalaryDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetSalaryDto>>();
            try
            {
                var dbContent = await _context.Salaries.ToListAsync();
                serviceResponse.Data = dbContent.Select(e => _mapper.Map<GetSalaryDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSalaryDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSalaryDto>();
            try
            {
                var dbContent = await _context.Salaries.FirstOrDefaultAsync(e => e.Id == id);
                serviceResponse.Data = _mapper.Map<GetSalaryDto>(dbContent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetSalaryDto>>> Create(CreateSalaryDto newSalary)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetSalaryDto>>();
            try
            {
                Salary salary = _mapper.Map<Salary>(newSalary);
                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Salaries.Select(e => _mapper.Map<GetSalaryDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSalaryDto>> Update(UpdateSalaryDto updatedSalary)
        {
            var serviceResponse = new ServiceResponse<GetSalaryDto>();
            try
            {
                Salary salary = await _context.Salaries.FirstOrDefaultAsync(e => e.Id == updatedSalary.Id);

                _mapper.Map(updatedSalary, salary);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetSalaryDto>(salary);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetSalaryDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetSalaryDto>>();
            try
            {
                Salary salary = await _context.Salaries.FirstAsync(e => e.Id == id);
                _context.Salaries.Remove(salary);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Salaries.Select(e => _mapper.Map<GetSalaryDto>(e)).ToList();
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