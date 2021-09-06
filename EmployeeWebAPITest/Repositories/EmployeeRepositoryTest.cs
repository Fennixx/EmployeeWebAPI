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
    public class EmployeeRepositoryTest : IDisposable
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        public EmployeeRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            context = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_WithNotExistingEmployee_ReturnEmpty()
        {
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithExistingEmployee_ReturnAllEmployee()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedEmployee = new Employee() { Id = randomId };

            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            context.Employees.Add(expectedEmployee);
            context.SaveChanges();

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().ContainEquivalentOf(mappedEmployeeDto,
            options => options.ComparingByMembers<GetEmployeeDto>());

            context.Employees.Remove(expectedEmployee);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WithNotExistingEmployeeId_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedEmployee = new Employee() { Id = randomId };
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(expectedEmployee))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.GetById(It.IsAny<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WithExistingEmployeeId_ReturnExpectedEmployee()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedEmployee = new Employee() { Id = randomId };
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.GetById(expectedEmployee.Id);

            response.Data.Should().BeEquivalentTo(mappedEmployeeDto,
            options => options.ComparingByMembers<GetEmployeeDto>());
        }

        [Fact]
        public async Task Create_WithEmployee_ReturnAllEmployeeWithCreatedOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedEmployee = new Employee() { Id = randomId };
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);
            mapperStub.Setup(mapper => mapper.Map<Employee>(It.IsAny<CreateEmployeeDto>()))
                .Returns(expectedEmployee);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Create(It.IsAny<CreateEmployeeDto>());

            response.Data.Should().ContainEquivalentOf(mappedEmployeeDto,
            options => options.ComparingByMembers<GetEmployeeDto>());

            context.Employees.Remove(expectedEmployee);
            context.SaveChanges();
        }

        [Fact]
        public async Task Create_WithNull_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedEmployee = new Employee() { Id = randomId };
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsNotNull<Employee>()))
                .Returns(mappedEmployeeDto);
            mapperStub.Setup(mapper => mapper.Map<Employee>(It.IsNotNull<CreateEmployeeDto>()))
                .Returns(expectedEmployee);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Create(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task Update_WithNotExistingEmployee_ReturnNull()
        {
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Update(It.IsNotNull<UpdateEmployeeDto>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateEmployee_WithNull_ReturnNull()
        {
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Update(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateEmployee_WithExistingEmployee_ReturnUpdatedEmployee()
        {
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Update(new UpdateEmployeeDto() { Id = 4 });

            response.Data.Should().BeEquivalentTo(mappedEmployeeDto,
            options => options.ComparingByMembers<GetEmployeeDto>());
        }

        [Fact]
        public async Task Delete_WithNotExistingEmployee_ReturnNull()
        {
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Delete(It.IsNotNull<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteEmployee_WithExistingEmployee_ReturnAllEmployeeWithoutTheOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedEmployee = new Employee() { Id = randomId };
            var mappedEmployeeDto = new GetEmployeeDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetEmployeeDto>(It.IsAny<Employee>()))
                .Returns(mappedEmployeeDto);

            context.Employees.Add(expectedEmployee);
            context.SaveChanges();

            var repo = new EmployeeRepository(mapperStub.Object, context);

            var response = await repo.Delete(randomId);

            response.Data.Should().NotContainEquivalentOf(mappedEmployeeDto,
            options => options.ComparingByMembers<GetEmployeeDto>());
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}