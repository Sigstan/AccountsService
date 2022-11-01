using AccountsService.Models.Accounts;
using AccountsService.Models.Operations;
using AccountsService.Storage.Entities;

namespace AccountsService.Core.Extensions.Models
{
    public static class ModelsExtensions
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
                Level = entity.Level.ToModel(),
                Id = entity.Id
            };
        }

        public static Operation ToEntity(this OperationModel model, Guid accountId)
        {
            return new Operation()
            {
               Currency = model.Currency.ToEntity(),
               Amount = model.Amount,
               Date = model.Date,
               Id = Guid.NewGuid(),
               Type = model.Type.ToEntity(),
               AccountId = accountId,
            };
        }
    }
}