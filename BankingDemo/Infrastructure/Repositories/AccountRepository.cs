using BankingDemo.Domain.Abstractions;
using BankingDemo.Domain.Entities;
using BankingDemo.Infrastructure.Repositories.Base;

namespace BankingDemo.Infrastructure.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRespository<Account>
{
    public AccountRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}