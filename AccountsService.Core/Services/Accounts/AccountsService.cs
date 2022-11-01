using AccountsService.Core.Exceptions;
using AccountsService.Core.Repositories.Accounts;
using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;

namespace AccountsService.Core.Services.Accounts
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<AccountStatusModel> GetAccountStatus(int accountNumber, CancellationToken cancellationToken)
        {
            var account = await GetAccountByAccountNumber(accountNumber, cancellationToken);

            return new AccountStatusModel()
            {
                Status = account.Status,
                AccountNumber = accountNumber
            };
        }

        public async Task<AccountModel> GetAccountBalance(int accountNumber, CancellationToken cancellationToken)
        {
            return await GetAccountByAccountNumber(accountNumber, cancellationToken);
        }

        public async Task SetAccountLevel(int accountNumber, AccountLevel level, CancellationToken cancellationToken)
        {
            var account = await GetAccountByAccountNumber(accountNumber, cancellationToken);
            account.Level = level;
            
            await _accountsRepository.Update(account, cancellationToken);
        }

        private async Task<AccountModel> GetAccountByAccountNumber(int accountNumber, CancellationToken cancellationToken)
        {
            var account = await _accountsRepository.GetAccountByAccountNumber(accountNumber, cancellationToken);

            if (account == null)
                throw new DomainException(DomainErrorCode.NotFound,
                    $"Account with number {accountNumber} was not found");

            return account;
        }
    }
}
