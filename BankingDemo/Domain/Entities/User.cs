namespace BankingDemo.Domain.Entities;
public record User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual List<Account> Accounts { get; set; } = new List<Account>();
}
