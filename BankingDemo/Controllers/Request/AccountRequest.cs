namespace BankingDemo.Controllers.Request;
public record AccountRequest
{
    public Guid userId { get; set; }
    public Guid accountId { get; set; }
}