using BankingDemo.Domain.Abstractions.Base;

namespace BankingDemo.Domain.Abstractions;
public interface IUserRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
}