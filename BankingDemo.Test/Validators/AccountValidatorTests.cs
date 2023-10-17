using BankingDemo.Service.Validators;

namespace BankingDemo.Test.Validators;
public class AccountValidatorTests
{
    private readonly AccountValidator _accountValidator;

    public AccountValidatorTests()
    {
        _accountValidator = new AccountValidator();
    }

    [Fact]
    public void ValidateAsync_AccountHasLessThanMinimumBalance_ThrowsException()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var amount = 120;
        var balance = 90;

        // Act and Assert
        Assert.Throws<Exception>(() => _accountValidator.Validate(balance, amount));
    }

    [Fact]
    public void ValidateAsync_WithdrawalExceeds90PercentLimit_ThrowsException()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var balance = 1000;
        var amount = balance * 0.91m;

        // Act and Assert
        Assert.Throws<Exception>(() => _accountValidator.Validate(balance, amount));
    }

    [Fact]
    public void ValidateAsync_ValidWithdrawal_DoesNotThrowException()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var balance = 1000;
        var amount = balance * 0.5m;

        // Act and Assert
        _accountValidator.Validate(balance, amount);
    }

    [Fact]
    public void ValidateAsync_ValidDeposit_DoesNotThrowException()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var balance = 1000;
        var amount = 500; // Valid deposit amount

        // Act and Assert
        _accountValidator.Validate(balance, amount);
    }

    [Fact]
    public void ValidateAsync_AccountHasMinimumBalance_NoExceptionThrown()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var balance = 200;

        // Act and Assert
        _accountValidator.Validate(balance, 100);
    }
}
