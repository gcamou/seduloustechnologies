namespace BankingDemo.Controllers.Request;
public record UserRequest
{
    public int userId { get; set; }
    public string Name { get; set; }
}