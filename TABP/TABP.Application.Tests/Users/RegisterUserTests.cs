using AutoFixture;
using FluentAssertions;
using Moq;
using TABP.Application.Common;
using TABP.Application.Roles.Common;
using TABP.Application.Users.Commands.Register;
using TABP.Application.Users.Common.Errors;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Repositories;
using TABP.Domain.Interfaces.Services;
namespace TABP.Application.Tests.Users;
public class RegisterUserTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly RegisterUserCommandHandler _handler;
    private readonly IFixture _fixture;
    public RegisterUserTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _handler = new RegisterUserCommandHandler(
            _userRepositoryMock.Object,
            _roleRepositoryMock.Object,
            _passwordHasherMock.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
    [Fact]
    public async Task Handle_ValidRequest_ShouldRegisterUserSuccessfully()
    {
        // Arrange
        var command = _fixture.Build<RegisterUserCommand>()
                              .With(x => x.RoleName, "Customer")
                              .With(x => x.Password, "SecurePassword123!")
                              .Create();
        var role = _fixture.Build<Role>()
                           .With(r => r.Name, "Customer")
                           .Create();
        var hashedPassword = "hashed_password";
        var salt = "salt_value";
        _roleRepositoryMock.Setup(r => r.GetRoleByNameAsync(command.RoleName, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(role);
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((User?)null);
        _passwordHasherMock.Setup(h => h.HashPassword(command.Password))
                           .Returns((hashedPassword, salt));
        _userRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(_fixture.Create<User>());
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }
    [Fact]
    public async Task Handle_UserAlreadyExists_ShouldReturnFailure()
    {
        // Arrange
        var command = _fixture.Create<RegisterUserCommand>();
        var existingUser = _fixture.Create<User>();
        _roleRepositoryMock.Setup(r => r.GetRoleByNameAsync(command.RoleName, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(_fixture.Create<Role>());
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingUser);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.UserAlreadyExist);
    }
    [Fact]
    public async Task Handle_RoleNotFound_ShouldReturnFailure()
    {
        // Arrange
        var command = _fixture.Create<RegisterUserCommand>();
        _roleRepositoryMock.Setup(r => r.GetRoleByNameAsync(command.RoleName, It.IsAny<CancellationToken>()))!
                           .ReturnsAsync((Role?)null);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(RoleErrors.RoleNotFound);
    }
}