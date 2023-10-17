using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingDemo.Controllers.Request;
using BankingDemo.Domain.Abstractions.Services;
using BankingDemo.Domain.Abstractions.Services.IValidators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountRequest request)
    {
        if (request.userId == Guid.Empty)
            return BadRequest("userId should be a valid Id");

        var account = await _accountService.CreateAsync(request.userId);

        return account != null ? Json(account) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] AccountRequest request)
    {
        if (request.accountId == Guid.Empty)
            return BadRequest("accountId should be a valid Id");

        var account = await _accountService.DeleteAccountAsync(request.accountId);

        return account ? Json(account) : StatusCode(StatusCodes.Status500InternalServerError);
    }


    [HttpPost("Deposit")]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
    {
        var result = await _accountService.DepositAsync(request.accountId, request.amount);

        return result ? Json(request) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("Withdraw")]
    public async Task<IActionResult> WithdrawAsync([FromBody] WithdrawRequest request)
    {
        var result = await _accountService.WithdrawAsync(request.accountId, request.amount);

        return result ? Json(request) : StatusCode(StatusCodes.Status500InternalServerError);
    }
}