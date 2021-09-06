using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using EmployeeWebAPI.Data;
using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeWebAPITest.Repositories
{
    public class JobCategoryRepositoryTest : IDisposable
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        public JobCategoryRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            context = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_WithNotExistingJobCategory_ReturnEmpty()
        {
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithExistingJobCategory_ReturnAllJobCategory()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedJobCategory = new JobCategory() { Id = randomId };

            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            context.JobCategories.Add(expectedJobCategory);
            context.SaveChanges();

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().ContainEquivalentOf(mappedJobCategoryDto,
            options => options.ComparingByMembers<GetJobCategoryDto>());

            context.JobCategories.Remove(expectedJobCategory);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WithNotExistingJobCategoryId_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedJobCategory = new JobCategory() { Id = randomId };
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(expectedJobCategory))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.GetById(It.IsAny<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WithExistingJobCategoryId_ReturnExpectedJobCategory()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedJobCategory = new JobCategory() { Id = randomId };
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.GetById(expectedJobCategory.Id);

            response.Data.Should().BeEquivalentTo(mappedJobCategoryDto,
            options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        [Fact]
        public async Task Create_WithJobCategory_ReturnAllJobCategoryWithCreatedOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedJobCategory = new JobCategory() { Id = randomId };
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);
            mapperStub.Setup(mapper => mapper.Map<JobCategory>(It.IsAny<CreateJobCategoryDto>()))
                .Returns(expectedJobCategory);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Create(It.IsAny<CreateJobCategoryDto>());

            response.Data.Should().ContainEquivalentOf(mappedJobCategoryDto,
            options => options.ComparingByMembers<GetJobCategoryDto>());

            context.JobCategories.Remove(expectedJobCategory);
            context.SaveChanges();
        }

        [Fact]
        public async Task Create_WithNull_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedJobCategory = new JobCategory() { Id = randomId };
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsNotNull<JobCategory>()))
                .Returns(mappedJobCategoryDto);
            mapperStub.Setup(mapper => mapper.Map<JobCategory>(It.IsNotNull<CreateJobCategoryDto>()))
                .Returns(expectedJobCategory);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Create(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task Update_WithNotExistingJobCategory_ReturnNull()
        {
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Update(It.IsNotNull<UpdateJobCategoryDto>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateJobCategory_WithNull_ReturnNull()
        {
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Update(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateJobCategory_WithExistingJobCategory_ReturnUpdatedJobCategory()
        {
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Update(new UpdateJobCategoryDto() { Id = 4 });

            response.Data.Should().BeEquivalentTo(mappedJobCategoryDto,
            options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        [Fact]
        public async Task Delete_WithNotExistingJobCategory_ReturnNull()
        {
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Delete(It.IsNotNull<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteJobCategory_WithExistingJobCategory_ReturnAllJobCategoryWithoutTheOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedJobCategory = new JobCategory() { Id = randomId };
            var mappedJobCategoryDto = new GetJobCategoryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetJobCategoryDto>(It.IsAny<JobCategory>()))
                .Returns(mappedJobCategoryDto);

            context.JobCategories.Add(expectedJobCategory);
            context.SaveChanges();

            var repo = new JobCategoryRepository(mapperStub.Object, context);

            var response = await repo.Delete(randomId);

            response.Data.Should().NotContainEquivalentOf(mappedJobCategoryDto,
            options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}