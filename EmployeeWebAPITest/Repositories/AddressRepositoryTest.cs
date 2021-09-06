using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using EmployeeWebAPI.Data;
using EmployeeWebAPI.Dtos;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeWebAPITest.Repositories
{
    public class AddressRepositoryTest : IDisposable
    {
        private DbContextOptions<DataContext> options;
        private DataContext context;

        public AddressRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
            context = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_WithNotExistingAddress_ReturnEmpty()
        {
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithExistingAddress_ReturnAllAddress()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedAddress = new Address() { Id = randomId };

            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            context.Addresses.Add(expectedAddress);
            context.SaveChanges();

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.GetAll();

            response.Data.Should().ContainEquivalentOf(mappedAddressDto,
            options => options.ComparingByMembers<GetAddressDto>());

            context.Addresses.Remove(expectedAddress);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetById_WithNotExistingAddressId_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedAddress = new Address() { Id = randomId };
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(expectedAddress))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.GetById(It.IsAny<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetById_WithExistingAddressId_ReturnExpectedAddress()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedAddress = new Address() { Id = randomId };
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.GetById(expectedAddress.Id);

            response.Data.Should().BeEquivalentTo(mappedAddressDto,
            options => options.ComparingByMembers<GetAddressDto>());
        }

        [Fact]
        public async Task Create_WithAddress_ReturnAllAddressWithCreatedOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedAddress = new Address() { Id = randomId };
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);
            mapperStub.Setup(mapper => mapper.Map<Address>(It.IsAny<CreateAddressDto>()))
                .Returns(expectedAddress);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Create(It.IsAny<CreateAddressDto>());

            response.Data.Should().ContainEquivalentOf(mappedAddressDto,
            options => options.ComparingByMembers<GetAddressDto>());

            context.Addresses.Remove(expectedAddress);
            context.SaveChanges();
        }

        [Fact]
        public async Task Create_WithNull_ReturnNull()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedAddress = new Address() { Id = randomId };
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsNotNull<Address>()))
                .Returns(mappedAddressDto);
            mapperStub.Setup(mapper => mapper.Map<Address>(It.IsNotNull<CreateAddressDto>()))
                .Returns(expectedAddress);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Create(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task Update_WithNotExistingAddress_ReturnNull()
        {
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Update(It.IsNotNull<UpdateAddressDto>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAddress_WithNull_ReturnNull()
        {
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Update(null);

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAddress_WithExistingAddress_ReturnUpdatedAddress()
        {
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Update(new UpdateAddressDto() { Id = 4 });

            response.Data.Should().BeEquivalentTo(mappedAddressDto,
            options => options.ComparingByMembers<GetAddressDto>());
        }

        [Fact]
        public async Task Delete_WithNotExistingAddress_ReturnNull()
        {
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Delete(It.IsNotNull<int>());

            response.Data.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAddress_WithExistingAddress_ReturnAllAddressWithoutTheOne()
        {
            var randomId = new Random().Next(100, 100000);
            var expectedAddress = new Address() { Id = randomId };
            var mappedAddressDto = new GetAddressDto() { Id = 3 };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(mapper => mapper.Map<GetAddressDto>(It.IsAny<Address>()))
                .Returns(mappedAddressDto);

            context.Addresses.Add(expectedAddress);
            context.SaveChanges();

            var repo = new AddressRepository(mapperStub.Object, context);

            var response = await repo.Delete(randomId);

            response.Data.Should().NotContainEquivalentOf(mappedAddressDto,
            options => options.ComparingByMembers<GetAddressDto>());
        }

        public void Dispose()
        {
            this.context = null;
        }
    }
}