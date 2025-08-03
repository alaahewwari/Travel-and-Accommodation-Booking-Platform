using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Cities.Commands.Create;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Tests.Cities
{
    public class CreateCityTests
    {
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly CreateCityCommandHandler _handler;
        private readonly IFixture _fixture;
        public CreateCityTests()
        {
            _cityRepositoryMock = new Mock<ICityRepository>();
            _handler = new CreateCityCommandHandler(_cityRepositoryMock.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnCreatedCity()
        {
            // Arrange
            var command = _fixture.Create<CreateCityCommand>();
            var createdCity = new City
            {
                Id = 1,
                Name = command.Name,
                Country = command.Country,
                PostOffice = command.PostOffice,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            _cityRepositoryMock.Setup(repo => repo.CreateCityAsync(It.IsAny<City>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(createdCity);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be(command.Name);
            result.Value.Country.Should().Be(command.Country);
            result.Value.PostOffice.Should().Be(command.PostOffice);
        }
        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldReturnFailure()
        {
            // Arrange
            var command = _fixture.Create<CreateCityCommand>();
            _cityRepositoryMock.Setup(repo => repo.CreateCityAsync(It.IsAny<City>(), It.IsAny<CancellationToken>()))
                               .ThrowsAsync(new Exception("Database error"));
            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
        }
    }
}