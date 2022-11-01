using AccountsService.Models.Accounts;
using AccountsService.Storage.Entities;

namespace AccountsService.Core.Extensions.Models
{
    public static class AccountExtensions
    {
        public static AccountModel? ToModel(this Account? entity)
        {
            if (entity == null)
                return null;

            return new AccountModel()
            {
                AccountNumber = entity.AccountNumber,
                Status = entity.Status.ToModel(),
                Balance = entity.Balance,
                Currency = entity.Currency.ToModel(),
                Level = entity.Level.ToModel()
            };
        }
    }
}