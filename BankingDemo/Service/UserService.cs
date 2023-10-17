using BankingDemo.Domain.Abstractions;
using BankingDemo.Domain.Abstractions.Services;
using BankingDemo.Domain.Entities;

namespace BankingDemo.Service;

public class UserService : IUserService
{
    private readonly IUserRepository<User> _userRepository;
    public UserService(IUserRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> CreateAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var userAdded = await _userRepository.InsertAsync(user);

        return userAdded;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new Exception("Id not valid");
        }

        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var delete = await _userRepository.DeleteAsync(user);

        return delete;
    }

    public List<User> GetAll()
    {
        var users = _userRepository.GetAll().ToList();
        return users;
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new Exception("Id not valid");
        }

        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(user.Id);

        if (userToUpdate == null)
        {
            throw new ArgumentNullException(nameof(userToUpdate));
        }

        userToUpdate.Name = user.Name;
        userToUpdate.Accounts = user.Accounts;

        var update = await _userRepository.UpdateAsync(userToUpdate);

        if (!update)
        {
            throw new Exception("Update fails");
        }

        return true;
    }
}