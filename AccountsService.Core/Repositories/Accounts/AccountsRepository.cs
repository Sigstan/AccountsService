using AccountsService.Core.Exceptions;
using AccountsService.Core.Extensions.Models;
using AccountsService.Models.Accounts;
using AccountsService.Storage.Data;
using AccountsService.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountsService.Core.Repositories.Accounts
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountModel?> GetAccountByAccountNumber(int accountNumber,
            CancellationToken cancellationToken)
        {
            var entity = await GetByAccountNumber(accountNumber, cancellationToken);
            return entity.ToModel();
        }

        public async Task Update(AccountModel account, CancellationToken cancellationToken)
        {
            var entity = await GetByAccountNumber(account.AccountNumber, cancellationToken);

            if (entity == null)
                throw new DomainException(DomainErrorCode.NotFound,
                    $"Account with number {account.AccountNumber} was not found");

            entity.Currency = account.Currency.ToEntity();
            entity.Level = account.Level.ToEntity();
            entity.Status = account.Status.ToEntity();
            entity.Balance = account.Balance;

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Account?> GetByAccountNumber(int accountNumber, CancellationToken cancellationToken)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber,
                cancellationToken);
        }
    }
}