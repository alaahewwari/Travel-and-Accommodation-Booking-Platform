using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Hotels.Queries.GetFeaturedDeals;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Hotel;
namespace TABP.Application.Tests.Hotels
{
    public class GetHotelFeaturedDealsTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRoomClassRepository> _roomClassRepoMock;
        private readonly GetHotelFeaturedDealsQueryHandler _handler;
        public GetHotelFeaturedDealsTests()
        {
            _fixture = new Fixture();
            _roomClassRepoMock = new Mock<IRoomClassRepository>();
            _handler = new GetHotelFeaturedDealsQueryHandler(_roomClassRepoMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnFeaturedDeals()
        {
            // Arrange
            var query = new GetHotelFeaturedDealsQuery(Count: 3);
            var hotels = _fixture.CreateMany<FeaturedealsHotels>(query.Count);
            _roomClassRepoMock
                .Setup(repo => repo.GetFeaturedDealsInHotelsAsync(query.Count, It.IsAny<CancellationToken>()))
                .ReturnsAsync(hotels);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(query.Count);
        }
        [Fact]
        public async Task Handle_ShouldReturnEmpty_WhenNoDealsExist()
        {
            // Arrange
            var query = new GetHotelFeaturedDealsQuery(Count: 5);
            _roomClassRepoMock
                .Setup(repo => repo.GetFeaturedDealsInHotelsAsync(query.Count, It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }
    }
}