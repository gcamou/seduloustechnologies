namespace BankingDemo.Domain.Abstractions.Services.IValidators;
public interface IAccountValidator
{
    void Validate(decimal totalBalance, decimal amount);
}