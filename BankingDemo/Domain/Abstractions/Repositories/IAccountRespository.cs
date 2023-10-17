using BankingDemo.Domain.Abstractions.Base;

namespace BankingDemo.Domain.Abstractions;
public interface IAccountRespository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
}