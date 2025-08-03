using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Users.Login.Commands;
using TABP.Domain.Entites;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Tests.Users
{
    public class LoginUserTests
    {
        private readonly Mock<IJwtGenerator> _jwtGeneratorMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly LoginUserCommandHandler _handler;
        private readonly IFixture _fixture;
        public LoginUserTests()
        {
            _jwtGeneratorMock = new Mock<IJwtGenerator>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtGeneratorMock.Object);
            _fixture = new Fixture();
        }
        [Fact]
        public async Task Handle_ValidCredentials_ShouldReturnToken()
        {
            //arrange 
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var user = _fixture.Build<User>()
                .With(u => u.Username, username)
                .With(u => u.PasswordHash, password)
                .Create();
            var token = _fixture.Create<string>();
            _userRepositoryMock
                .Setup(repo => repo.AuthenticateUserAsync(username, password, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _jwtGeneratorMock
                .Setup(gen => gen.GenerateToken(user))
                .Returns(token);
            //act
            var command = new LoginUserCommand(username, password);
            var result = await _handler.Handle(command, CancellationToken.None);
            //assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Token.Should().Be(token);
        }
        [Fact]
        public async Task Handle_InvalidCredentials_ShouldReturnFailure()
        {
            //arrange 
            var username = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            _userRepositoryMock
                .Setup(repo => repo.AuthenticateUserAsync(username, password, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);
            //act
            var command = new LoginUserCommand(username, password);
            var result = await _handler.Handle(command, CancellationToken.None);
            //assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be("Login.InvalidCredentials");
            result.Error.Description.Should().Be("The username or password is incorrect.");
        }
    }
}