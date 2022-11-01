using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;
using AccountsService.Models.Operations;

namespace AccountsService.Core.Services.Accounts
{
    public interface IAccountsService
    {
        Task<AccountStatusModel> GetAccountStatus(int accountNumber, CancellationToken cancellationToken);
        Task<AccountModel> GetAccountBalance(int accountNumber, CancellationToken cancellationToken);
        Task SetAccountLevel(int accountNumber, AccountLevel level, CancellationToken cancellationToken);
        Task<AccountModel> ValidateAndGetAccountForOperation(int accountNumber, OperationModel operationModel, CancellationToken cancellationToken);
        Task Update(AccountModel account, CancellationToken cancellationToken);
    }
}
