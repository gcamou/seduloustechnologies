namespace BankingDemo.Controllers.Request;

public record DepositRequest
{
    public Guid accountId { get; set; }
    public decimal amount { get; set; }
}