using BankingDemo.Domain.Entities;
using BankingDemo.Infrastructure;
using BankingDemo.Infrastructure.Repositories.Base;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace BankingDemo.Test.Repositories;

public class UserRepositoryTest
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public UserRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GenericRepositoryTestDatabase")
            .Options;
    }

    [Fact]
    public async Task Should_Returns_All_Entities()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<User>(dbContext);

            // Add test data
            var users = Builder<User>.CreateListOfSize(5)
                                        .All()
                                        .With(user => user.Id = Guid.NewGuid())
                                        .Build();

            dbContext.Users.AddRange(users);
            await dbContext.SaveChangesAsync();

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 1);
        }
    }

    [Fact]
    public async Task Should_Return_User_When_Id_Is_Valid()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<User>(dbContext);

            var user = Builder<User>.CreateNew()
                                    .With(user => user.Id = Guid.NewGuid())
                                    .Build();
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }
    }

    [Fact]
    public async Task Should_Create_A_User()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<User>(dbContext);

            var user = Builder<User>.CreateNew()
                                    .With(user => user.Id = Guid.NewGuid())
                                    .Build();
            // Act
            var result = await repository.InsertAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }
    }

    [Fact]
    public async Task Should_Update_User()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<User>(dbContext);

            var user = Builder<User>.CreateNew()
                                    .With(user => user.Id = Guid.NewGuid())
                                    .Build();
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            user.Name = "UpdatedName";

            // Act
            var result = await repository.UpdateAsync(user);

            // Assert
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Should_Delete_The_User()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<User>(dbContext);

            var user = Builder<User>.CreateNew()
                                    .With(user => user.Id = Guid.NewGuid())
                                    .Build();
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.DeleteAsync(user);

            // Assert
            Assert.True(result);
            Assert.Null(await repository.GetByIdAsync(user.Id));
        }
    }
}
