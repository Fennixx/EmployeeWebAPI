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
    public class SalaryRepositoryTest : IDisposable
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        public SalaryRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            context = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_WithNotExistingSalary_ReturnEmpty()
        {
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithExistingSalary_ReturnAllSalary()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedSalary = new Salary() { Id = randomId };

            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            context.Salaries.Add(expectedSalary);
            context.SaveChanges();

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().ContainEquivalentOf(mappedSalaryDto,
            options => options.ComparingByMembers<GetSalaryDto>());

            context.Salaries.Remove(expectedSalary);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WithNotExistingSalaryId_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedSalary = new Salary() { Id = randomId };
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(expectedSalary))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.GetById(It.IsAny<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WithExistingSalaryId_ReturnExpectedSalary()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedSalary = new Salary() { Id = randomId };
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.GetById(expectedSalary.Id);

            response.Data.Should().BeEquivalentTo(mappedSalaryDto,
            options => options.ComparingByMembers<GetSalaryDto>());
        }

        [Fact]
        public async Task Create_WithSalary_ReturnAllSalaryWithCreatedOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedSalary = new Salary() { Id = randomId };
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);
            mapperStub.Setup(mapper => mapper.Map<Salary>(It.IsAny<CreateSalaryDto>()))
                .Returns(expectedSalary);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Create(It.IsAny<CreateSalaryDto>());

            response.Data.Should().ContainEquivalentOf(mappedSalaryDto,
            options => options.ComparingByMembers<GetSalaryDto>());

            context.Salaries.Remove(expectedSalary);
            context.SaveChanges();
        }

        [Fact]
        public async Task Create_WithNull_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedSalary = new Salary() { Id = randomId };
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsNotNull<Salary>()))
                .Returns(mappedSalaryDto);
            mapperStub.Setup(mapper => mapper.Map<Salary>(It.IsNotNull<CreateSalaryDto>()))
                .Returns(expectedSalary);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Create(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task Update_WithNotExistingSalary_ReturnNull()
        {
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Update(It.IsNotNull<UpdateSalaryDto>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateSalary_WithNull_ReturnNull()
        {
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Update(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateSalary_WithExistingSalary_ReturnUpdatedSalary()
        {
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Update(new UpdateSalaryDto() { Id = 4 });

            response.Data.Should().BeEquivalentTo(mappedSalaryDto,
            options => options.ComparingByMembers<GetSalaryDto>());
        }

        [Fact]
        public async Task Delete_WithNotExistingSalary_ReturnNull()
        {
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Delete(It.IsNotNull<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteSalary_WithExistingSalary_ReturnAllSalaryWithoutTheOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedSalary = new Salary() { Id = randomId };
            var mappedSalaryDto = new GetSalaryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetSalaryDto>(It.IsAny<Salary>()))
                .Returns(mappedSalaryDto);

            context.Salaries.Add(expectedSalary);
            context.SaveChanges();

            var repo = new SalaryRepository(mapperStub.Object, context);

            var response = await repo.Delete(randomId);

            response.Data.Should().NotContainEquivalentOf(mappedSalaryDto,
            options => options.ComparingByMembers<GetSalaryDto>());
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}