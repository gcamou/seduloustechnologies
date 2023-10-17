namespace BankingDemo.Controllers.Request;
public record WithdrawRequest
{
    public Guid accountId { get; set; }
    public decimal amount { get; set; }
}