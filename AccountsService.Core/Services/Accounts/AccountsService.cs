using AccountsService.Core.Exceptions;
using AccountsService.Core.Repositories.Accounts;
using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;
using AccountsService.Models.Operations;

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
                AccountNumber = accountNumber,
                Id = account.Id
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

        public async Task<AccountModel> ValidateAndGetAccountForOperation(int accountNumber, OperationModel operationModel, CancellationToken cancellationToken)
        {
            var account = await GetAccountByAccountNumber(accountNumber, cancellationToken);

            if (account.Status != AccountStatus.Open)
                throw new DomainException(DomainErrorCode.InvalidOperation,
                    $"Can not perform operations because account is in {account.Status} status");

            if (account.Currency != operationModel.Currency)
                throw new DomainException(DomainErrorCode.InvalidCurrency,
                    $"Can not perform operations with {operationModel.Currency} currency");

            return account;
        }

        public async Task Update(AccountModel accountModel, CancellationToken cancellationToken)
        {
            var account = await GetAccountByAccountNumber(accountModel.AccountNumber, cancellationToken);
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
