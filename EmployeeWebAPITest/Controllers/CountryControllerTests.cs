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
    public class CountryControllerTests
    {
        [Fact]
        public async Task GetAll_WithNotExistingCountry_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCountryDto>> { Data = null });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_WithExistingCountry_ReturnAllCountry()
        {
            // Arrange
            var expectedCountry = new GetCountryDto { Id = 3 };
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCountryDto>> { Data = new List<GetCountryDto>() { expectedCountry } });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<IEnumerable<GetCountryDto>>> response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetCountryDto>>>()
                .Data.Should().ContainEquivalentOf(expectedCountry,
                options => options.ComparingByMembers<GetCountryDto>());
        }

        [Fact]
        public async Task GetOne_WithNotExistingCountryId_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetCountryDto> { Data = null });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetOne_WithExistingCountryId_ReturnCountryById()
        {
            // Arrange
            var expectedCountry = new GetCountryDto { Id = 3 };
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetCountryDto> { Data = expectedCountry });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<GetCountryDto>> response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetCountryDto>>()
                .Data.Should().BeEquivalentTo(expectedCountry,
                options => options.ComparingByMembers<GetCountryDto>());
        }

        [Fact]
        public async Task CreateCountry_WithCountry_ReturnAllCountryWithCreatedOne()
        {
            // Arrange
            var expectedCountry = new GetCountryDto { Id = 3 };
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Create(It.IsAny<CreateCountryDto>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCountryDto>> { Data = new List<GetCountryDto>() { expectedCountry } });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.CreateCountry(new CreateCountryDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetCountryDto>>>()
                .Data.Should().ContainEquivalentOf(expectedCountry,
                options => options.ComparingByMembers<GetCountryDto>());
        }

        [Fact]
        public async Task CreateCountry_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Create(null))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCountryDto>> { Data = null });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.CreateCountry(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateCountry_WithNotExistingCountry_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateCountryDto>()))
                .ReturnsAsync(new ServiceResponse<GetCountryDto> { Data = null });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateCountry(new UpdateCountryDto());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateCountry_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Update(null))
                .ReturnsAsync(new ServiceResponse<GetCountryDto> { Data = null });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateCountry(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateCountry_WithExistingCountry_ReturnUpdatedCountry()
        {
            // Arrange
            var expectedCountry = new GetCountryDto { Id = 3 };
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateCountryDto>()))
                .ReturnsAsync(new ServiceResponse<GetCountryDto> { Data = expectedCountry });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateCountry(new UpdateCountryDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetCountryDto>>()
                .Data.Should().BeEquivalentTo(expectedCountry,
                options => options.ComparingByMembers<GetCountryDto>());
        }

        [Fact]
        public async Task DeleteCountry_WithNotExistingCountry_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCountryDto>> { Data = null });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteCountry_WithExistingCountry_ReturnAllCountryWithoutTheOne()
        {
            // Arrange
            var expectedCountry = new GetCountryDto { Id = 3 };
            var repositoryStub = new Mock<ICountryRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCountryDto>> { Data = new List<GetCountryDto>() });

            var controller = new CountryController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetCountryDto>>>()
                .Data.Should().NotContainEquivalentOf(expectedCountry,
                options => options.ComparingByMembers<GetCountryDto>());
        }
    }
}