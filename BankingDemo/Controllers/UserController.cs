using BankingDemo.Controllers.Request;
using BankingDemo.Domain.Abstractions.Services;
using BankingDemo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
    {
        if (string.IsNullOrEmpty(request.Name.Trim()))
            return BadRequest("Name should be not null or empty");

        var user = new User()
        {
            Name = request.Name,
        };
        
        var userAdded = await _userService.CreateAsync(user);

        return userAdded != null ? Json(userAdded) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("All")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetAll();

        return Ok(users);    
    }
}