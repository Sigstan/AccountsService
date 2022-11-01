using AccountsService.Models.Accounts;

namespace AccountsService.Core.Repositories.Accounts
{
    public interface IAccountsRepository
    {
        Task<AccountModel?> GetAccountByAccountNumber(int accountNumber, CancellationToken cancellationToken);
        Task Update(AccountModel account, CancellationToken cancellationToken);
    }
}
