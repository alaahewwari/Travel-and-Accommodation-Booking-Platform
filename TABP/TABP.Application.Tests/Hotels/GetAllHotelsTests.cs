using AutoFixture;
using FluentAssertions;
using Moq;
using Sieve.Models;
using TABP.Application.Hotels.Mapper;
using TABP.Application.Hotels.Queries.GetAll;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Models.Common;
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
        public async Task Handle_WhenHotelsExist_ReturnsMappedPagedResponses()
        {
            // Arrange
            var query = new GetAllHotelsQuery(
                Filters: "name@=*inn",
                Sorts: "-StarRating",
                Page: 1,
                PageSize: 10
            );
            var hotels = _fixture.CreateMany<HotelForManagement>(4).ToList();
            var paged = new PagedResult<HotelForManagement>
            {
                Items = hotels,
                PaginationMetadata = new PaginationMetadata(
                    TotalCount: 4,
                    TotalPages: 1,
                    CurrentPage: 1,
                    PageSize: 10
                )
            };
            _hotelRepoMock
                .Setup(x => x.GetAllHotelsAsync(It.IsAny<SieveModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paged);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().BeEquivalentTo(hotels);
            result.Value.PaginationMetadata.Should().BeEquivalentTo(paged.PaginationMetadata);
            _hotelRepoMock.Verify(
                x => x.GetAllHotelsAsync(It.IsAny<SieveModel>(), CancellationToken.None),
                Times.Once
            );
        }
        [Fact]
        public async Task Handle_WhenNoHotels_ReturnsEmptyPagedList()
        {
            // Arrange
            var query = new GetAllHotelsQuery(
                Filters: null,
                Sorts: null,
                Page: 1,
                PageSize: 10
            );
            var paged = new PagedResult<HotelForManagement>
            {
                Items = new List<HotelForManagement>(),
                PaginationMetadata = new PaginationMetadata(
                    TotalCount: 0,
                    TotalPages: 0,
                    PageSize: 10,
                    CurrentPage: 1
                )
            };
            _hotelRepoMock
                .Setup(x => x.GetAllHotelsAsync(It.IsAny<SieveModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paged);
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().BeEmpty();
            result.Value.PaginationMetadata.TotalCount.Should().Be(0);
            result.Value.PaginationMetadata.TotalPages.Should().Be(0);
        }
        [Fact]
        public async Task Handle_RepositoryThrowsException_BubblesUp()
        {
            // Arrange
            var query = new GetAllHotelsQuery(
                Filters: null,
                Sorts: null,
                Page: 1,
                PageSize: 10
            );

            _hotelRepoMock
                .Setup(x => x.GetAllHotelsAsync(It.IsAny<SieveModel>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new TimeoutException("timeout"));
            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
            // Assert
            await act.Should()
                .ThrowExactlyAsync<TimeoutException>()
                .WithMessage("timeout");
        }
    }
}