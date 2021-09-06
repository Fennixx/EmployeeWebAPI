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
    public class CityControllerTests
    {
        [Fact]
        public async Task GetAll_WithNotExistingCity_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCityDto>> { Data = null });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_WithExistingCity_ReturnAllCity()
        {
            // Arrange
            var expectedCity = new GetCityDto { Id = 3 };
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCityDto>> { Data = new List<GetCityDto>() { expectedCity } });

            var controller = new CityController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<IEnumerable<GetCityDto>>> response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetCityDto>>>()
                .Data.Should().ContainEquivalentOf(expectedCity,
                options => options.ComparingByMembers<GetCityDto>());
        }

        [Fact]
        public async Task GetOne_WithNotExistingCityId_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetCityDto> { Data = null });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetOne_WithExistingCityId_ReturnCityById()
        {
            // Arrange
            var expectedCity = new GetCityDto { Id = 3 };
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetCityDto> { Data = expectedCity });

            var controller = new CityController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<GetCityDto>> response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetCityDto>>()
                .Data.Should().BeEquivalentTo(expectedCity,
                options => options.ComparingByMembers<GetCityDto>());
        }

        [Fact]
        public async Task CreateCity_WithCity_ReturnAllCityWithCreatedOne()
        {
            // Arrange
            var expectedCity = new GetCityDto { Id = 3 };
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Create(It.IsAny<CreateCityDto>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCityDto>> { Data = new List<GetCityDto>() { expectedCity } });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.CreateCity(new CreateCityDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetCityDto>>>()
                .Data.Should().ContainEquivalentOf(expectedCity,
                options => options.ComparingByMembers<GetCityDto>());
        }

        [Fact]
        public async Task CreateCity_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Create(null))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCityDto>> { Data = null });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.CreateCity(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateCity_WithNotExistingCity_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateCityDto>()))
                .ReturnsAsync(new ServiceResponse<GetCityDto> { Data = null });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateCity(new UpdateCityDto());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateCity_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Update(null))
                .ReturnsAsync(new ServiceResponse<GetCityDto> { Data = null });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateCity(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateCity_WithExistingCity_ReturnUpdatedCity()
        {
            // Arrange
            var expectedCity = new GetCityDto { Id = 3 };
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateCityDto>()))
                .ReturnsAsync(new ServiceResponse<GetCityDto> { Data = expectedCity });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateCity(new UpdateCityDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetCityDto>>()
                .Data.Should().BeEquivalentTo(expectedCity,
                options => options.ComparingByMembers<GetCityDto>());
        }

        [Fact]
        public async Task DeleteCity_WithNotExistingCity_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCityDto>> { Data = null });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteCity_WithExistingCity_ReturnAllCityWithoutTheOne()
        {
            // Arrange
            var expectedCity = new GetCityDto { Id = 3 };
            var repositoryStub = new Mock<ICityRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetCityDto>> { Data = new List<GetCityDto>() });

            var controller = new CityController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetCityDto>>>()
                .Data.Should().NotContainEquivalentOf(expectedCity,
                options => options.ComparingByMembers<GetCityDto>());
        }
    }
}