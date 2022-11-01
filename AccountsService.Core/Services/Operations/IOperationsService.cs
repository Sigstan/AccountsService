using AccountsService.Models.Accounts;
using AccountsService.Models.Operations;

namespace AccountsService.Core.Services.Operations
{
    public interface IOperationsService
    {
        Task<AccountModel> ExecuteOperation(int accountNumber, OperationModel operationModel, CancellationToken cancellationToken);
    }
}
