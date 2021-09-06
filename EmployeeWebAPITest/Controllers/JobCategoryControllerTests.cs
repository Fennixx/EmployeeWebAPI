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
    public class JobCategoryControllerTests
    {
        [Fact]
        public async Task GetAll_WithNotExistingJobCategory_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetJobCategoryDto>> { Data = null });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_WithExistingJobCategory_ReturnAllJobCategory()
        {
            // Arrange
            var expectedJobCategory = new GetJobCategoryDto { Id = 3 };
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.GetAll())
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetJobCategoryDto>> { Data = new List<GetJobCategoryDto>() { expectedJobCategory } });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<IEnumerable<GetJobCategoryDto>>> response = await controller.GetAll();

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetJobCategoryDto>>>()
                .Data.Should().ContainEquivalentOf(expectedJobCategory,
                options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        [Fact]
        public async Task GetOne_WithNotExistingJobCategoryId_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetJobCategoryDto> { Data = null });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetOne_WithExistingJobCategoryId_ReturnJobCategoryById()
        {
            // Arrange
            var expectedJobCategory = new GetJobCategoryDto { Id = 3 };
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<GetJobCategoryDto> { Data = expectedJobCategory });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            ActionResult<ServiceResponse<GetJobCategoryDto>> response = await controller.GetOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetJobCategoryDto>>()
                .Data.Should().BeEquivalentTo(expectedJobCategory,
                options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        [Fact]
        public async Task CreateJobCategory_WithJobCategory_ReturnAllJobCategoryWithCreatedOne()
        {
            // Arrange
            var expectedJobCategory = new GetJobCategoryDto { Id = 3 };
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Create(It.IsAny<CreateJobCategoryDto>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetJobCategoryDto>> { Data = new List<GetJobCategoryDto>() { expectedJobCategory } });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.CreateJobCategory(new CreateJobCategoryDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetJobCategoryDto>>>()
                .Data.Should().ContainEquivalentOf(expectedJobCategory,
                options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        [Fact]
        public async Task CreateJobCategory_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Create(null))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetJobCategoryDto>> { Data = null });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.CreateJobCategory(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateJobCategory_WithNotExistingJobCategory_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateJobCategoryDto>()))
                .ReturnsAsync(new ServiceResponse<GetJobCategoryDto> { Data = null });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateJobCategory(new UpdateJobCategoryDto());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task UpdateJobCategory_WithNull_ReturnBadRequest()
        {
            // Arrange
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Update(null))
                .ReturnsAsync(new ServiceResponse<GetJobCategoryDto> { Data = null });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateJobCategory(null);

            // Assert
            response.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateJobCategory_WithExistingJobCategory_ReturnUpdatedJobCategory()
        {
            // Arrange
            var expectedJobCategory = new GetJobCategoryDto { Id = 3 };
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Update(It.IsAny<UpdateJobCategoryDto>()))
                .ReturnsAsync(new ServiceResponse<GetJobCategoryDto> { Data = expectedJobCategory });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.UpdateJobCategory(new UpdateJobCategoryDto());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<GetJobCategoryDto>>()
                .Data.Should().BeEquivalentTo(expectedJobCategory,
                options => options.ComparingByMembers<GetJobCategoryDto>());
        }

        [Fact]
        public async Task DeleteJobCategory_WithNotExistingJobCategory_ReturnNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetJobCategoryDto>> { Data = null });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DeleteJobCategory_WithExistingJobCategory_ReturnAllJobCategoryWithoutTheOne()
        {
            // Arrange
            var expectedJobCategory = new GetJobCategoryDto { Id = 3 };
            var repositoryStub = new Mock<IJobCategoryRepository>();
            repositoryStub.Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(new ServiceResponse<IEnumerable<GetJobCategoryDto>> { Data = new List<GetJobCategoryDto>() });

            var controller = new JobCategoryController(repositoryStub.Object);

            // Act
            var response = await controller.DeleteOne(It.IsAny<int>());

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Result.As<OkObjectResult>()
                .Value.As<ServiceResponse<IEnumerable<GetJobCategoryDto>>>()
                .Data.Should().NotContainEquivalentOf(expectedJobCategory,
                options => options.ComparingByMembers<GetJobCategoryDto>());
        }
    }
}