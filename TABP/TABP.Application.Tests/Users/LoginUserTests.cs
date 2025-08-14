using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Users.Commands.Login;
using TABP.Application.Users.Common;
using TABP.Domain.Entities;
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
            _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = _fixture.Create<User>();
            var token = _fixture.Create<string>();
            _userRepositoryMock
                .Setup(repo => repo.AuthenticateUserAsync(user.Username, user.PasswordHash, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _jwtGeneratorMock
                .Setup(gen => gen.GenerateToken(user))
                .Returns(token);
            //act
            var command = new LoginUserCommand(user.Username, user.PasswordHash);
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
            result.Error.Code.Should().Be(UserErrors.InvalidCredentials.Code);
            result.Error.Description.Should().Be(UserErrors.InvalidCredentials.Description);
        }
    }
}