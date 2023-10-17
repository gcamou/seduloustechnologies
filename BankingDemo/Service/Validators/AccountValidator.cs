using BankingDemo.Domain.Abstractions.Services.IValidators;

namespace BankingDemo.Service.Validators;

public class AccountValidator : IAccountValidator
{
    public void Validate(decimal totalBalance, decimal amount)
    {
        if (totalBalance - amount < 100)
            throw new Exception("An account cannot have less than $100 at any time in an account.");

        if (!IsWithinWithdrawalLimit(totalBalance, amount))
            throw new Exception("Cannot withdraw more than 90% of their total balance");
    }

    private bool IsWithinWithdrawalLimit(decimal totalBalance, decimal withdrawalAmount)
    {
        decimal maxWithdrawalAllowed = totalBalance * 0.9m;

        return withdrawalAmount <= maxWithdrawalAllowed;
    }
}