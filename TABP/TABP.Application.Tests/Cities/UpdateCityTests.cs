using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Cities.Commands.Update;
using TABP.Application.Cities.Common;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Tests.Cities
{
    public class UpdateCityTests
    {
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly UpdateCityCommandHandler _handler;
        private readonly IFixture _fixture;
        public UpdateCityTests()
        {
            _cityRepositoryMock = new Mock<ICityRepository>();
            _handler = new UpdateCityCommandHandler(_cityRepositoryMock.Object);
            _fixture = new Fixture();
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Fact]
        public async Task Handle_ValidUpdate_ShouldReturnUpdatedCity()
        {
            // Arrange
            var existingCity = _fixture.Create<City>();
            var updatedCity = new City
            {
                Id = existingCity.Id,
                Name = "UpdatedName",
                Country = "UpdatedCountry",
                PostOffice = "54321",
                CreatedAt = existingCity.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
            };
            var command = new UpdateCityCommand(
                Id: existingCity.Id,
                Name: updatedCity.Name,
                Country: updatedCity.Country,
                PostOffice: updatedCity.PostOffice
            );
            _cityRepositoryMock.Setup(r => r.GetCityByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(existingCity);
            _cityRepositoryMock.Setup(r => r.UpdateCityAsync(It.IsAny<City>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(updatedCity);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Name.Should().Be("UpdatedName");
            result.Value.Country.Should().Be("UpdatedCountry");
            result.Value.PostOffice.Should().Be("54321");
        }
        [Fact]
        public async Task Handle_CityNotFound_ShouldReturnFailure()
        {
            // Arrange
            var command = _fixture.Create<UpdateCityCommand>();
            _cityRepositoryMock.Setup(r => r.GetCityByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync((City?)null);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(CityErrors.CityNotFound);
        }
        [Fact]
        public async Task Handle_UpdateFails_ShouldReturnNotModifiedError()
        {
            // Arrange
            var existingCity = _fixture.Create<City>();
            var command = new UpdateCityCommand(
                Id: existingCity.Id,
                Name: "NewName",
                Country: "NewCountry",
                PostOffice: "11111"
            );
            _cityRepositoryMock.Setup(r => r.GetCityByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(existingCity);
            _cityRepositoryMock.Setup(r => r.UpdateCityAsync(It.IsAny<City>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync((City?)null); // simulate update failure
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(CityErrors.NotModified);
        }
    }
}