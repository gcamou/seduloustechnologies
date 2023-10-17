using BankingDemo.Domain.Abstractions;
using BankingDemo.Domain.Abstractions.Services;
using BankingDemo.Domain.Abstractions.Services.IValidators;
using BankingDemo.Domain.Entities;

namespace BankingDemo.Service;

public class AccountService : IAccountService
{
    private readonly IAccountRespository<Account> _accountRepository;
    private readonly IUserService _userService;
    private readonly IAccountValidator _accountValidator;

    public AccountService(
        IAccountRespository<Account> accountRepository,
        IUserService userService,
        IAccountValidator accountValidator)
    {
        _accountRepository = accountRepository;
        _userService = userService;
        _accountValidator = accountValidator;
    }
    public async Task<Account> CreateAsync(Guid userId)
    {
        if (userId != Guid.Empty)
        {
            throw new Exception("userId not valid");
        }

        var user = await _userService.GetUserAsync(userId);

        var account = new Account()
        {
            UserId = userId,
        };

        var accountAdded = await _accountRepository.InsertAsync(account);

        return accountAdded;
    }

    public async Task<bool> DeleteAccountAsync(Guid accountId)
    {
        if (accountId != Guid.Empty)
        {
            throw new Exception("accountId not valid");
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        var deleted = await _accountRepository.DeleteAsync(account);

        return deleted;
    }

    public async Task<decimal> GetBalanceAsync(Guid accountId)
    {
        if (accountId != Guid.Empty)
        {
            throw new Exception("accountId not valid");
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        return account.Balance;
    }

    public async Task<bool> DepositAsync(Guid accountId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        account.Balance += amount;
        var updated = await _accountRepository.UpdateAsync(account);

        return updated;
    }

    public async Task<bool> WithdrawAsync(Guid accountId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        _accountValidator.Validate(account.Balance, amount);

        account.Balance -= amount;

        var updated = await _accountRepository.UpdateAsync(account);
        
        return updated;
    }
}