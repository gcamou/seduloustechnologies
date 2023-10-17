using BankingDemo.Domain.Abstractions;
using BankingDemo.Domain.Entities;
using BankingDemo.Infrastructure.Repositories.Base;

namespace BankingDemo.Infrastructure.Repositories;
public class UserRepository: GenericRepository<User>, IUserRepository<User>
{
    public UserRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}