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
    public class JobCategoryRepository : IJobCategoryRepository
    {
        protected readonly IMapper _mapper;
        private readonly DataContext _context;

        public JobCategoryRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<GetJobCategoryDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetJobCategoryDto>>();
            try
            {
                var dbContent = await _context.JobCategories.ToListAsync();
                serviceResponse.Data = dbContent.Select(e => _mapper.Map<GetJobCategoryDto>(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJobCategoryDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetJobCategoryDto>();
            try
            {
                var dbContent = await _context.JobCategories.FirstOrDefaultAsync(e => e.Id == id);
                serviceResponse.Data = _mapper.Map<GetJobCategoryDto>(dbContent);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetJobCategoryDto>>> Create(CreateJobCategoryDto newJobCategory)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetJobCategoryDto>>();
            try
            {
                JobCategory jobCategory = _mapper.Map<JobCategory>(newJobCategory);
                _context.JobCategories.Add(jobCategory);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.JobCategories.Select(e => _mapper.Map<GetJobCategoryDto>(e)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetJobCategoryDto>> Update(UpdateJobCategoryDto updatedJobCategory)
        {
            var serviceResponse = new ServiceResponse<GetJobCategoryDto>();
            try
            {
                JobCategory jobCategory = await _context.JobCategories.FirstOrDefaultAsync(e => e.Id == updatedJobCategory.Id);

                _mapper.Map(updatedJobCategory, jobCategory);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetJobCategoryDto>(jobCategory);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GetJobCategoryDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<GetJobCategoryDto>>();
            try
            {
                JobCategory jobCategory = await _context.JobCategories.FirstAsync(e => e.Id == id);
                _context.JobCategories.Remove(jobCategory);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.JobCategories.Select(e => _mapper.Map<GetJobCategoryDto>(e)).ToList();
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