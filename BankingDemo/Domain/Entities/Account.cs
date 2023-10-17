namespace BankingDemo.Domain.Entities;
public record Account
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public Guid UserId { get; set; }
}