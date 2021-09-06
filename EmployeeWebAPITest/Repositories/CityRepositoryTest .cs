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
    public class CityRepositoryTest : IDisposable
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        public CityRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            context = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_WithNotExistingCity_ReturnEmpty()
        {
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithExistingCity_ReturnAllCity()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCity = new City() { Id = randomId };

            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            context.Cities.Add(expectedCity);
            context.SaveChanges();

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().ContainEquivalentOf(mappedCityDto,
            options => options.ComparingByMembers<GetCityDto>());

            context.Cities.Remove(expectedCity);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WithNotExistingCityId_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCity = new City() { Id = randomId };
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(expectedCity))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.GetById(It.IsAny<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WithExistingCityId_ReturnExpectedCity()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCity = new City() { Id = randomId };
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.GetById(expectedCity.Id);

            response.Data.Should().BeEquivalentTo(mappedCityDto,
            options => options.ComparingByMembers<GetCityDto>());
        }

        [Fact]
        public async Task Create_WithCity_ReturnAllCityWithCreatedOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCity = new City() { Id = randomId };
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);
            mapperStub.Setup(mapper => mapper.Map<City>(It.IsAny<CreateCityDto>()))
                .Returns(expectedCity);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Create(It.IsAny<CreateCityDto>());

            response.Data.Should().ContainEquivalentOf(mappedCityDto,
            options => options.ComparingByMembers<GetCityDto>());

            context.Cities.Remove(expectedCity);
            context.SaveChanges();
        }

        [Fact]
        public async Task Create_WithNull_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCity = new City() { Id = randomId };
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsNotNull<City>()))
                .Returns(mappedCityDto);
            mapperStub.Setup(mapper => mapper.Map<City>(It.IsNotNull<CreateCityDto>()))
                .Returns(expectedCity);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Create(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task Update_WithNotExistingCity_ReturnNull()
        {
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Update(It.IsNotNull<UpdateCityDto>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateCity_WithNull_ReturnNull()
        {
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Update(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateCity_WithExistingCity_ReturnUpdatedCity()
        {
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Update(new UpdateCityDto() { Id = 4 });

            response.Data.Should().BeEquivalentTo(mappedCityDto,
            options => options.ComparingByMembers<GetCityDto>());
        }

        [Fact]
        public async Task Delete_WithNotExistingCity_ReturnNull()
        {
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Delete(It.IsNotNull<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteCity_WithExistingCity_ReturnAllCityWithoutTheOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCity = new City() { Id = randomId };
            var mappedCityDto = new GetCityDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCityDto>(It.IsAny<City>()))
                .Returns(mappedCityDto);

            context.Cities.Add(expectedCity);
            context.SaveChanges();

            var repo = new CityRepository(mapperStub.Object, context);

            var response = await repo.Delete(randomId);

            response.Data.Should().NotContainEquivalentOf(mappedCityDto,
            options => options.ComparingByMembers<GetCityDto>());
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}