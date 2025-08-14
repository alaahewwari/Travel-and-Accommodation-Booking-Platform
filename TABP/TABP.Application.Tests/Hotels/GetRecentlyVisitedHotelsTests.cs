using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Hotels.Mapper;
using TABP.Application.Hotels.Queries.GetAll;
using TABP.Application.Hotels.Queries.GetRecentlyVisited;
using TABP.Application.Users.Common;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
namespace TABP.Application.Tests.Hotels
{
    public class GetRecentlyVisitedHotelsTests
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IHotelRepository> _hotelRepoMock = new();
        private readonly GetRecentlyVisitedHotelsQueryHandler _handler;
        private readonly Fixture _fixture;
        public GetRecentlyVisitedHotelsTests()
        {
            _handler = new GetRecentlyVisitedHotelsQueryHandler(_userRepoMock.Object, _hotelRepoMock.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Fact]
        public async Task Handle_ValidRequest_ReturnsHotelResponses()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsQuery(UserId: 1, Count: 3);
            var user = _fixture.Create<User>();
            var hotels = _fixture.CreateMany<Hotel>(3).ToList();
            _userRepoMock.Setup(x => x.GetUserByIdAsync(query.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _hotelRepoMock.Setup(x => x.GetRecentlyVisitedHotelsAsync(query.UserId, query.Count, It.IsAny<CancellationToken>())).ReturnsAsync(hotels);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(3);
            result.Value.Should().BeEquivalentTo(hotels.Select(h => h.ToHotelResponse()));
            _userRepoMock.Verify(x => x.GetUserByIdAsync(1, CancellationToken.None), Times.Once);
            _hotelRepoMock.Verify(x => x.GetRecentlyVisitedHotelsAsync(1, 3, CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task Handle_UserNotFound_ReturnsFailure()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsQuery(UserId: 42, Count: 5);
            _userRepoMock.Setup(x => x.GetUserByIdAsync(query.UserId, It.IsAny<CancellationToken>())).ReturnsAsync((User?)null);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(UserErrors.UserNotFound.Code);
            result.Error.Description.Should().Be(UserErrors.UserNotFound.Description);
            _userRepoMock.Verify(x => x.GetUserByIdAsync(42, CancellationToken.None), Times.Once);
            _hotelRepoMock.Verify(x => x.GetRecentlyVisitedHotelsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async Task Handle_NoHotels_ReturnsEmptyList()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsQuery(UserId: 7, Count: 2);
            var user = _fixture.Create<User>();
            _userRepoMock.Setup(x => x.GetUserByIdAsync(query.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _hotelRepoMock.Setup(x => x.GetRecentlyVisitedHotelsAsync(query.UserId, query.Count, It.IsAny<CancellationToken>())).ReturnsAsync(new List<Hotel>());
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
            _hotelRepoMock.Verify(x => x.GetRecentlyVisitedHotelsAsync(7, 2, CancellationToken.None), Times.Once);
        }
    }
}