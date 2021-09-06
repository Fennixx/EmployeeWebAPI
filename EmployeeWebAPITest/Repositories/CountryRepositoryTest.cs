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
    public class CountryRepositoryTest : IDisposable
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        public CountryRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            context = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_WithNotExistingCountry_ReturnEmpty()
        {
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithExistingCountry_ReturnAllCountry()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCountry = new Country() { Id = randomId };

            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            context.Countries.Add(expectedCountry);
            context.SaveChanges();

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().ContainEquivalentOf(mappedCountryDto,
            options => options.ComparingByMembers<GetCountryDto>());

            context.Countries.Remove(expectedCountry);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WithNotExistingCountryId_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCountry = new Country() { Id = randomId };
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(expectedCountry))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.GetById(It.IsAny<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WithExistingCountryId_ReturnExpectedCountry()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCountry = new Country() { Id = randomId };
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.GetById(expectedCountry.Id);

            response.Data.Should().BeEquivalentTo(mappedCountryDto,
            options => options.ComparingByMembers<GetCountryDto>());
        }

        [Fact]
        public async Task Create_WithCountry_ReturnAllCountryWithCreatedOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCountry = new Country() { Id = randomId };
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);
            mapperStub.Setup(mapper => mapper.Map<Country>(It.IsAny<CreateCountryDto>()))
                .Returns(expectedCountry);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Create(It.IsAny<CreateCountryDto>());

            response.Data.Should().ContainEquivalentOf(mappedCountryDto,
            options => options.ComparingByMembers<GetCountryDto>());

            context.Countries.Remove(expectedCountry);
            context.SaveChanges();
        }

        [Fact]
        public async Task Create_WithNull_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCountry = new Country() { Id = randomId };
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsNotNull<Country>()))
                .Returns(mappedCountryDto);
            mapperStub.Setup(mapper => mapper.Map<Country>(It.IsNotNull<CreateCountryDto>()))
                .Returns(expectedCountry);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Create(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task Update_WithNotExistingCountry_ReturnNull()
        {
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Update(It.IsNotNull<UpdateCountryDto>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateCountry_WithNull_ReturnNull()
        {
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Update(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateCountry_WithExistingCountry_ReturnUpdatedCountry()
        {
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Update(new UpdateCountryDto() { Id = 4 });

            response.Data.Should().BeEquivalentTo(mappedCountryDto,
            options => options.ComparingByMembers<GetCountryDto>());
        }

        [Fact]
        public async Task Delete_WithNotExistingCountry_ReturnNull()
        {
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Delete(It.IsNotNull<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteCountry_WithExistingCountry_ReturnAllCountryWithoutTheOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedCountry = new Country() { Id = randomId };
            var mappedCountryDto = new GetCountryDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetCountryDto>(It.IsAny<Country>()))
                .Returns(mappedCountryDto);

            context.Countries.Add(expectedCountry);
            context.SaveChanges();

            var repo = new CountryRepository(mapperStub.Object, context);

            var response = await repo.Delete(randomId);

            response.Data.Should().NotContainEquivalentOf(mappedCountryDto,
            options => options.ComparingByMembers<GetCountryDto>());
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}