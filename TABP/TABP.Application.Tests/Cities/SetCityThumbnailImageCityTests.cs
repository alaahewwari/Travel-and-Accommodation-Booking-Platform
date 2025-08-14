using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Cities.Commands.SetThumbnail;
using TABP.Application.Cities.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Tests.Cities;
public class SetCityThumbnailTests
{
    private readonly Mock<IImageRepository<CityImage>> _imageRepoMock;
    private readonly Mock<ICityRepository> _cityRepoMock;
    private readonly Mock<ICloudinaryService> _cloudinaryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly SetCityThumbnailCommandHandler _handler;
    private readonly IFixture _fixture;
    public SetCityThumbnailTests()
    {
        _imageRepoMock = new Mock<IImageRepository<CityImage>>();
        _cityRepoMock = new Mock<ICityRepository>();
        _cloudinaryMock = new Mock<ICloudinaryService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new SetCityThumbnailCommandHandler(
            _imageRepoMock.Object,
            _cityRepoMock.Object,
            _cloudinaryMock.Object,
            _unitOfWorkMock.Object
        );
        _fixture = new Fixture();
        _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
    [Fact]
    public async Task Handle_ValidRequest_ShouldUploadAndReplaceThumbnail()
    {
        // Arrange
        var command = new SetCityThumbnailCommand(
            CityId: 1,
            FileStream: new MemoryStream(new byte[] { 1, 2, 3 }),
            FileName: "test-image.jpg"
        );
        var imageUrl = "https://cloudinary.com/image/city.jpg";
        var city = _fixture.Build<City>().With(c => c.Id, 1).Create();
        _cityRepoMock.Setup(r => r.GetCityByIdAsync(command.CityId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(city);
        _imageRepoMock.Setup(r => r.ExistsAsync(command.CityId, ImageType.Thumbnail, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true);
        _imageRepoMock.Setup(r => r.DeleteAsync(command.CityId, ImageType.Thumbnail, It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);
        _cloudinaryMock.Setup(c => c.UploadImageAsync(It.IsAny<Stream>(), command.FileName, "cities", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(imageUrl);
        _imageRepoMock.Setup(r => r.CreateAsync(It.IsAny<CityImage>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.ExecuteResilientTransactionAsync(It.IsAny<Func<CancellationToken, Task>>(), It.IsAny<CancellationToken>()))
                       .Returns<Func<CancellationToken, Task>, CancellationToken>((func, token) => func(token));
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(imageUrl);
        _imageRepoMock.Verify(r => r.DeleteAsync(command.CityId, ImageType.Thumbnail, It.IsAny<CancellationToken>()), Times.Once);
        _cloudinaryMock.Verify(c => c.UploadImageAsync(It.IsAny<Stream>(), command.FileName, "cities", It.IsAny<CancellationToken>()), Times.Once);
        _imageRepoMock.Verify(r => r.CreateAsync(It.IsAny<CityImage>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task Handle_CityDoesNotExist_ShouldReturnCityNotFound()
    {
        // Arrange
        var fileStream = new MemoryStream();
        var command = new SetCityThumbnailCommand(99, fileStream, "city.jpg");
        _cityRepoMock.Setup(r => r.GetCityByIdAsync(command.CityId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((City?)null);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(CityErrors.CityNotFound);
        _imageRepoMock.Verify(r => r.DeleteAsync(It.IsAny<long>(), It.IsAny<ImageType>(), It.IsAny<CancellationToken>()), Times.Never);
        _cloudinaryMock.Verify(c => c.UploadImageAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}