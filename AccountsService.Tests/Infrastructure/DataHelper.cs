using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;
using AccountsService.Models.Operations;

namespace AccountsService.Tests.Infrastructure
{
    public static class DataHelper
    {
        public static OperationModel GetOperationModel(decimal amount, OperationType operationType, Currency currency = Currency.Eur) => new()
        {
            Amount = amount,
            Currency = currency,
            Date = DateTime.UtcNow,
            Type = operationType
        };

        public static AccountModel GetAccount(AccountLevel level, decimal balance,
            AccountStatus status = AccountStatus.Open) => new()
        {
            AccountNumber = 1001,
            Balance = balance,
            Currency = Currency.Eur,
            Id = Guid.NewGuid(),
            Level = level,
            Status = status
        };
    }
}
