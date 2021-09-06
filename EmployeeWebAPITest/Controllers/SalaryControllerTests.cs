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
    public class SalaryControllerTests
    {
        [Fact]
        public async Task GetAll_WithNotExistingSalary_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetSalaryDto>> { Data = null });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_WithExistingSalary_ReturnAllSalary()
        {
            // Arrange
            var expectedSalary = new GetSalaryDto { Id = 3 };
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetSalaryDto>> { Data = new List<GetSalaryDto>() { expectedSalary } });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<IEnumerable<GetSalaryDto>>> response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetSalaryDto>>>()
                .Data.Should().ContainEquivalentOf(expectedSalary,
                options => options.ComparingByMembers<GetSalaryDto>());
        }

        [Fact]
        public async Task GetOne_WithNotExistingSalaryId_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetSalaryDto> { Data = null });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetOne_WithExistingSalaryId_ReturnSalaryById()
        {
            // Arrange
            var expectedSalary = new GetSalaryDto { Id = 3 };
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetSalaryDto> { Data = expectedSalary });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<GetSalaryDto>> response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetSalaryDto>>()
                .Data.Should().BeEquivalentTo(expectedSalary,
                options => options.ComparingByMembers<GetSalaryDto>());
        }

        [Fact]
        public async Task CreateSalary_WithSalary_ReturnAllSalaryWithCreatedOne()
        {
            // Arrange
            var expectedSalary = new GetSalaryDto { Id = 3 };
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Create(It.IsAny<CreateSalaryDto>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetSalaryDto>> { Data = new List<GetSalaryDto>() { expectedSalary } });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.CreateSalary(new CreateSalaryDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetSalaryDto>>>()
                .Data.Should().ContainEquivalentOf(expectedSalary,
                options => options.ComparingByMembers<GetSalaryDto>());
        }

        [Fact]
        public async Task CreateSalary_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Create(null))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetSalaryDto>> { Data = null });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.CreateSalary(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateSalary_WithNotExistingSalary_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateSalaryDto>()))
                .ReturnsAsync(new ServiceResponse<GetSalaryDto> { Data = null });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateSalary(new UpdateSalaryDto());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateSalary_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Update(null))
                .ReturnsAsync(new ServiceResponse<GetSalaryDto> { Data = null });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateSalary(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateSalary_WithExistingSalary_ReturnUpdatedSalary()
        {
            // Arrange
            var expectedSalary = new GetSalaryDto { Id = 3 };
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateSalaryDto>()))
                .ReturnsAsync(new ServiceResponse<GetSalaryDto> { Data = expectedSalary });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateSalary(new UpdateSalaryDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetSalaryDto>>()
                .Data.Should().BeEquivalentTo(expectedSalary,
                options => options.ComparingByMembers<GetSalaryDto>());
        }

        [Fact]
        public async Task DeleteSalary_WithNotExistingSalary_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetSalaryDto>> { Data = null });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteSalary_WithExistingSalary_ReturnAllSalaryWithoutTheOne()
        {
            // Arrange
            var expectedSalary = new GetSalaryDto { Id = 3 };
            var repositoryStub = new Mock<ISalaryRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetSalaryDto>> { Data = new List<GetSalaryDto>() });

            var controller = new SalaryController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetSalaryDto>>>()
                .Data.Should().NotContainEquivalentOf(expectedSalary,
                options => options.ComparingByMembers<GetSalaryDto>());
        }
    }
}