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
    public class AddressControllerTests
    {
        [Fact]
        public async Task GetAll_WithNotExistingAddress_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetAddressDto>> { Data = null });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_WithExistingAddress_ReturnAllAddress()
        {
            // Arrange
            var expectedAddress = new GetAddressDto { Id = 3, Street1 = "Test" };
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetAddressDto>> { Data = new List<GetAddressDto>() { expectedAddress } });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<IEnumerable<GetAddressDto>>> response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetAddressDto>>>()
                .Data.Should().ContainEquivalentOf(expectedAddress,
                options => options.ComparingByMembers<GetAddressDto>());
        }

        [Fact]
        public async Task GetOne_WithNotExistingAddressId_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetAddressDto> { Data = null });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetOne_WithExistingAddressId_ReturnAddressById()
        {
            // Arrange
            var expectedAddress = new GetAddressDto { Id = 3, Street1 = "Test" };
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetAddressDto> { Data = expectedAddress });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<GetAddressDto>> response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetAddressDto>>()
                .Data.Should().BeEquivalentTo(expectedAddress,
                options => options.ComparingByMembers<GetAddressDto>());
        }

        [Fact]
        public async Task CreateAddress_WithAddress_ReturnAllAddressWithCreatedOne()
        {
            // Arrange
            var expectedAddress = new GetAddressDto { Id = 3, Street1 = "Test" };
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Create(It.IsAny<CreateAddressDto>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetAddressDto>> { Data = new List<GetAddressDto>() { expectedAddress } });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.CreateAddress(new CreateAddressDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetAddressDto>>>()
                .Data.Should().ContainEquivalentOf(expectedAddress,
                options => options.ComparingByMembers<GetAddressDto>());
        }

        [Fact]
        public async Task CreateAddress_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Create(null))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetAddressDto>> { Data = null });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.CreateAddress(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateAddress_WithNotExistingAddress_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateAddressDto>()))
                .ReturnsAsync(new ServiceResponse<GetAddressDto> { Data = null });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateAddress(new UpdateAddressDto());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateAddress_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Update(null))
                .ReturnsAsync(new ServiceResponse<GetAddressDto> { Data = null });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateAddress(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateAddress_WithExistingAddress_ReturnUpdatedAddress()
        {
            // Arrange
            var expectedAddress = new GetAddressDto { Id = 3, Street1 = "Test" };
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateAddressDto>()))
                .ReturnsAsync(new ServiceResponse<GetAddressDto> { Data = expectedAddress });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateAddress(new UpdateAddressDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetAddressDto>>()
                .Data.Should().BeEquivalentTo(expectedAddress,
                options => options.ComparingByMembers<GetAddressDto>());
        }

        [Fact]
        public async Task DeleteAddress_WithNotExistingAddress_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetAddressDto>> { Data = null });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteAddress_WithExistingAddress_ReturnAllAddressWithoutTheOne()
        {
            // Arrange
            var expectedAddress = new GetAddressDto { Id = 3, Street1 = "Test" };
            var repositoryStub = new Mock<IAddressRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetAddressDto>> { Data = new List<GetAddressDto>() });

            var controller = new AddressController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetAddressDto>>>()
                .Data.Should().NotContainEquivalentOf(expectedAddress,
                options => options.ComparingByMembers<GetAddressDto>());
        }
    }
}