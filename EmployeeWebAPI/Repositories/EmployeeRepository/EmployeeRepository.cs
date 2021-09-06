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
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmployeeRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<GetEmployeeDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetEmployeeDto>>();
            try
            {
                var dbContent = await _context.Employees.ToListAsync();
                serviceResponse.Data = dbContent.Select(e => _mapper.Map<GetEmployeeDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var dbContent = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
                serviceResponse.Data = _mapper.Map<GetEmployeeDto>(dbContent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetEmployeeDto>>> Create(CreateEmployeeDto newEmployee)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetEmployeeDto>>();
            try
            {
                Employee employee = _mapper.Map<Employee>(newEmployee);
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Employees.Select(e => _mapper.Map<GetEmployeeDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> Update(UpdateEmployeeDto updatedEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);

                _mapper.Map(updatedEmployee, employee);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetEmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetEmployeeDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetEmployeeDto>>();
            try
            {
                Employee employee = await _context.Employees.FirstAsync(e => e.Id == id);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.Select(e => _mapper.Map<GetEmployeeDto>(e)).ToList();
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