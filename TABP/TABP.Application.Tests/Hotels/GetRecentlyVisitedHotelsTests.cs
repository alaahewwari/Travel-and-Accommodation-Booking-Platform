using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TABP.Application.Hotels.Queries.GetRecentlyVisited;
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
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Fact]
        public async Task Handle_ValidRequest_ReturnsHotelResponses()
        {
            // Arrange
            var query = new GetRecentlyVisitedHotelsQuery(UserId: 1, Count: 3);
            var user = _fixture.Create<User>();
            var hotels = _fixture.CreateMany<Hotel>(3).ToList();
            _userRepoMock.Setup(x => x.GetUserByIdAsync(query.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _hotelRepoMock.Setup(x => x.GetRecentlyVisitedHotelsAsync(query.UserId, query.Count, It.IsAny<CancellationToken>()))
                .ReturnsAsync(hotels);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(3);
        }
    }
}
