using BankingDemo.Domain.Abstractions;
using BankingDemo.Domain.Entities;
using BankingDemo.Service;
using Moq;

namespace BankingDemo.Test.Services;
public class UserServiceTests
{
    private readonly Mock<IUserRepository<User>> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository<User>>();
        _userService = new UserService(_userRepositoryMock.Object);
    }
    [Fact]
    public async Task CreateAsync_ValidUser_CreatesUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Name = "John Doe" };

        _userRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<User>())).ReturnsAsync(user);

        // Act
        var result = await _userService.CreateAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task CreateAsync_NullUser_ThrowsException()
    {
        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.CreateAsync(null));
    }

    [Fact]
    public async Task CreateAsync_ValidUser_ReturnsCreatedUser()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Name = "John Doe" };
        var userAdded = new User { Id = user.Id, Name = user.Name };

        _userRepositoryMock.Setup(repo => repo.InsertAsync(user)).ReturnsAsync(userAdded);

        // Act
        var result = await _userService.CreateAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userAdded, result);
    }

    [Fact]
    public async Task CreateAsync_UserRepositoryThrowsException_ThrowsException()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Name = "John Doe" };

        _userRepositoryMock.Setup(repo => repo.InsertAsync(user)).ThrowsAsync(new Exception("Repository exception"));

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(user));
    }

    [Fact]
    public async Task DeleteAsync_ValidId_DeletesUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "John Doe" };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.DeleteAsync(user)).ReturnsAsync(true);

        // Act
        var result = await _userService.DeleteAsync(userId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_EmptyId_ThrowsException()
    {
        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.DeleteAsync(Guid.Empty));
    }

    [Fact]
    public async Task DeleteAsync_UserNotFound_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.DeleteAsync(userId));
    }

    [Fact]
    public async Task DeleteAsync_ValidId_CallsUserRepositoryDeleteAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "John Doe" };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.DeleteAsync(user)).ReturnsAsync(true);

        // Act
        await _userService.DeleteAsync(userId);

        // Assert
        _userRepositoryMock.Verify(repo => repo.DeleteAsync(user), Times.Once);
    }
    [Fact]
    public async Task UpdateAsync_ValidUser_UpdatesUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "John Doe", Accounts = null };
        var userToUpdate = new User { Id = userId, Name = "Updated Name", Accounts = null };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(userToUpdate);
        _userRepositoryMock.Setup(repo => repo.UpdateAsync(userToUpdate)).ReturnsAsync(true);

        // Act
        var result = await _userService.UpdateAsync(user);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_UserNotFound_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "John Doe", Accounts = null };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _userService.UpdateAsync(user));
    }

    [Fact]
    public async Task UpdateAsync_UserRepositoryUpdateReturnsFalse_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "John Doe", Accounts = null };
        var userToUpdate = new User { Id = userId, Name = "Updated Name", Accounts = null };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(userToUpdate);
        _userRepositoryMock.Setup(repo => repo.UpdateAsync(userToUpdate)).ReturnsAsync(false);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(user));
    }

    [Fact]
    public async Task UpdateAsync_ValidUser_CallsUserRepositoryUpdateAsync()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Name = "John Doe", Accounts = null };
        var userToUpdate = new User { Id = userId, Name = "Updated Name", Accounts = null };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(userToUpdate);
        _userRepositoryMock.Setup(repo => repo.UpdateAsync(userToUpdate)).ReturnsAsync(true);

        // Act
        await _userService.UpdateAsync(user);

        // Assert
        _userRepositoryMock.Verify(repo => repo.UpdateAsync(userToUpdate), Times.Once);
    }
}