using BankingDemo.Domain.Entities;

namespace BankingDemo.Domain.Abstractions.Services;

public interface IAccountService
{
    public Task<Account> CreateAsync(Guid userId);
    public Task<bool> DeleteAccountAsync(Guid accountId);
    public Task<decimal> GetBalanceAsync(Guid accountId);
    public Task<bool> DepositAsync(Guid accountId, decimal amount);
    public Task<bool> WithdrawAsync(Guid accountId, decimal amount);
}