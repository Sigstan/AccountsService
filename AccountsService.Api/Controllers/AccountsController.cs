using AccountsService.Core.Services.Accounts;
using AccountsService.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AccountsService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsServices;

        public AccountsController(IAccountsService accountsServices)
        {
            _accountsServices = accountsServices;
        }

        [HttpGet]
        [Route("{accountNumber}/status")]
        public async Task<IActionResult> GetAccountStatus(int accountNumber, CancellationToken cancellationToken)
        {
            var status = await _accountsServices.GetAccountStatus(accountNumber, cancellationToken);
            return Ok(status);
        }

        [HttpGet]
        [Route("{accountNumber}/balance")]
        public async Task<IActionResult> GetAccountBalance(int accountNumber, CancellationToken cancellationToken)
        {
            var balance = await _accountsServices.GetAccountBalance(accountNumber, cancellationToken);
            return Ok(balance);
        }

        [HttpPost]
        [Route("{accountNumber}/level")]
        public async Task<IActionResult> SetAccountLevel(int accountNumber, [FromBody] [EnumDataType(typeof(AccountLevel))] AccountLevel level,
            CancellationToken cancellationToken)
        {
            await _accountsServices.SetAccountLevel(accountNumber, level, cancellationToken);
            return Ok();
        }
    }
}