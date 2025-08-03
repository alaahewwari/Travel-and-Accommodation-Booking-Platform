using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Cities.Commands.Delete;
using TABP.Application.Cities.Common;
using TABP.Application.Common;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Tests.Cities;
public class DeleteCityTests
{
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly DeleteCityCommandHandler _handler;
    private readonly IFixture _fixture;
    public DeleteCityTests()
    {
        _cityRepositoryMock = new Mock<ICityRepository>();
        _handler = new DeleteCityCommandHandler(_cityRepositoryMock.Object);
        _fixture = new Fixture();
        _fixture.Behaviors
        .OfType<ThrowingRecursionBehavior>()
        .ToList()
        .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
    [Fact]
    public async Task Handle_ExistingCity_ShouldDeleteSuccessfully()
    {
        // Arrange
        var city = _fixture.Create<City>();
        var command = new DeleteCityCommand(city.Id);
        _cityRepositoryMock.Setup(r => r.GetCityByIdAsync(city.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(city);
        _cityRepositoryMock.Setup(r => r.DeleteCityAsync(city.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        _cityRepositoryMock.Verify(r => r.DeleteCityAsync(city.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task Handle_CityDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        var command = new DeleteCityCommand(_fixture.Create<int>());
        _cityRepositoryMock.Setup(r => r.GetCityByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((City?)null);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(CityErrors.CityNotFound);
        _cityRepositoryMock.Verify(r => r.DeleteCityAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}