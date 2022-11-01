using AccountsService.Models.Enums;

namespace AccountsService.Core.Repositories.Cashbacks
{
    public interface ICashbacksRepository
    {
        Task<decimal> GetCashbackPercentageByAccountLevel(AccountLevel accountLevel, CancellationToken cancellationToken);
    }
}
