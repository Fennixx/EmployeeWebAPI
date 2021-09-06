using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using EmployeeWebAPI.Controllers;
using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeWebAPITest.Controllers
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async Task GetAll_WithNotExistingEmployee_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetEmployeeDto>> { Data = null });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_WithExistingEmployee_ReturnAllEmployee()
        {
            // Arrange
            var expectedEmployee = new GetEmployeeDto { Id = 3 };
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetEmployeeDto>> { Data = new List<GetEmployeeDto>() { expectedEmployee } });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<IEnumerable<GetEmployeeDto>>> response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetEmployeeDto>>>()
                .Data.Should().ContainEquivalentOf(expectedEmployee,
                options => options.ComparingByMembers<GetEmployeeDto>());
        }

        [Fact]
        public async Task GetOne_WithNotExistingEmployeeId_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetEmployeeDto> { Data = null });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetOne_WithExistingEmployeeId_ReturnEmployeeById()
        {
            // Arrange
            var expectedEmployee = new GetEmployeeDto { Id = 3 };
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetEmployeeDto> { Data = expectedEmployee });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<GetEmployeeDto>> response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetEmployeeDto>>()
                .Data.Should().BeEquivalentTo(expectedEmployee,
                options => options.ComparingByMembers<GetEmployeeDto>());
        }

        [Fact]
        public async Task CreateEmployee_WithEmployee_ReturnAllEmployeeWithCreatedOne()
        {
            // Arrange
            var expectedEmployee = new GetEmployeeDto { Id = 3 };
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Create(It.IsAny<CreateEmployeeDto>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetEmployeeDto>> { Data = new List<GetEmployeeDto>() { expectedEmployee } });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.CreateEmployee(new CreateEmployeeDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetEmployeeDto>>>()
                .Data.Should().ContainEquivalentOf(expectedEmployee,
                options => options.ComparingByMembers<GetEmployeeDto>());
        }

        [Fact]
        public async Task CreateEmployee_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Create(null))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetEmployeeDto>> { Data = null });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.CreateEmployee(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateEmployee_WithNotExistingEmployee_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateEmployeeDto>()))
                .ReturnsAsync(new ServiceResponse<GetEmployeeDto> { Data = null });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateEmployee(new UpdateEmployeeDto());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateEmployee_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Update(null))
                .ReturnsAsync(new ServiceResponse<GetEmployeeDto> { Data = null });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateEmployee(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateEmployee_WithExistingEmployee_ReturnUpdatedEmployee()
        {
            // Arrange
            var expectedEmployee = new GetEmployeeDto { Id = 3 };
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateEmployeeDto>()))
                .ReturnsAsync(new ServiceResponse<GetEmployeeDto> { Data = expectedEmployee });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateEmployee(new UpdateEmployeeDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetEmployeeDto>>()
                .Data.Should().BeEquivalentTo(expectedEmployee,
                options => options.ComparingByMembers<GetEmployeeDto>());
        }

        [Fact]
        public async Task DeleteEmployee_WithNotExistingEmployee_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetEmployeeDto>> { Data = null });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteEmployee_WithExistingEmployee_ReturnAllEmployeeWithoutTheOne()
        {
            // Arrange
            var expectedEmployee = new GetEmployeeDto { Id = 3 };
            var repositoryStub = new Mock<IEmployeeRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetEmployeeDto>> { Data = new List<GetEmployeeDto>() });

            var controller = new EmployeeController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetEmployeeDto>>>()
                .Data.Should().NotContainEquivalentOf(expectedEmployee,
                options => options.ComparingByMembers<GetEmployeeDto>());
        }
    }
}