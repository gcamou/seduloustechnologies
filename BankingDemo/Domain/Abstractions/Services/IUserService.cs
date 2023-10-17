using BankingDemo.Domain.Entities;

namespace BankingDemo.Domain.Abstractions.Services;

public interface IUserService
{
    public Task<User> CreateAsync(User user);
    public Task<bool> DeleteAsync(Guid id);
    public Task<bool> UpdateAsync(User user);
    public Task<User> GetUserAsync(Guid id);
    public List<User> GetAll();
}