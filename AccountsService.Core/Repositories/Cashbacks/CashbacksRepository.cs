using AccountsService.Core.Exceptions;
using AccountsService.Core.Extensions.Models;
using AccountsService.Models.Enums;
using AccountsService.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountsService.Core.Repositories.Cashbacks
{
    public class CashbacksRepository : ICashbacksRepository
    {
        private readonly ApplicationDbContext _context;

        public CashbacksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetCashbackPercentageByAccountLevel(AccountLevel accountLevel, CancellationToken cancellationToken)
        {
            var cashbackPercentage = await _context.CashbackConfigurations
                .FirstOrDefaultAsync(x => x.AccountLevel == accountLevel.ToEntity(), cancellationToken);

            if (cashbackPercentage == null)
                throw new DomainException(DomainErrorCode.NotFound,
                    $"Cashback configuration for account level {accountLevel} was not found");

            return cashbackPercentage.CashbackPercentange;
        }
    }
}