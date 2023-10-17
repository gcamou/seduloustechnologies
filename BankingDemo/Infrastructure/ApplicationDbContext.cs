using BankingDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingDemo.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Account> Accounts{ get; set; }
}