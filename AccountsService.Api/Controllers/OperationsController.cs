using AccountsService.Core.Services.Operations;
using AccountsService.Models.Operations;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationsService _operationsService;

        public OperationsController(IOperationsService operationsService)
        {
            _operationsService = operationsService;
        }

        [HttpPost]
        [Route("{accountNumber}")]
        public async Task<IActionResult> ExecuteOperation(int accountNumber, OperationModel operationModel, CancellationToken cancellationToken)
        {
            var accountStatus = await _operationsService.ExecuteOperation(accountNumber, operationModel, cancellationToken);
            return Ok(accountStatus);
        }
    }
}