using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Hotels.Mapper;
using TABP.Application.Hotels.Queries.GetAll;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Hotel;
namespace TABP.Application.Tests.Hotels
{
    public class GetAllHotelsQueryHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepoMock = new();
        private readonly GetAllHotelsQueryHandler _handler;
        private readonly Fixture _fixture;
        public GetAllHotelsQueryHandlerTests()
        {
            _handler = new GetAllHotelsQueryHandler(_hotelRepoMock.Object);
            _fixture = new Fixture();
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Fact]
        public async Task Handle_WhenHotelsExist_ReturnsMappedResponses()
        {
            // Arrange
            var query = new GetAllHotelsQuery();
            var hotels = _fixture.CreateMany<HotelForManagement>(4).ToList();
            _hotelRepoMock.Setup(x => x.GetAllHotelsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(hotels);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(hotels.Select(h => h.ToHotelForManagementResponse()));
            _hotelRepoMock.Verify(x => x.GetAllHotelsAsync(CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task Handle_WhenNoHotels_ReturnsEmptyList()
        {
            // Arrange
            var query = new GetAllHotelsQuery();
            var hotels = new List<HotelForManagement>();
            _hotelRepoMock.Setup(x => x.GetAllHotelsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(hotels);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }
        [Fact]
        public async Task Handle_RepositoryThrowsException_BubblesUp()
        {
            // Arrange
            var query = new GetAllHotelsQuery();
            _hotelRepoMock.Setup(x => x.GetAllHotelsAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new TimeoutException("timeout"));
            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
            // Assert
            await act.Should().ThrowExactlyAsync<TimeoutException>().WithMessage("timeout");
        }
    }
}