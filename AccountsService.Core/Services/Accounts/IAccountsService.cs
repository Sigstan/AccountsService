using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;

namespace AccountsService.Core.Services.Accounts
{
    public interface IAccountsService
    {
        Task<AccountStatusModel> GetAccountStatus(int accountNumber, CancellationToken cancellationToken);
        Task<AccountModel> GetAccountBalance(int accountId, CancellationToken cancellationToken);
        Task SetAccountLevel(int accountNumber, AccountLevel level, CancellationToken cancellationToken);
    }
}
