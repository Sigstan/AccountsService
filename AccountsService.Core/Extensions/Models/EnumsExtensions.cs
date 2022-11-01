using AccountsService.Models.Enums;

namespace AccountsService.Core.Extensions.Models
{
    public static class EnumsExtensions
    {
        public static AccountLevel ToModel(this Storage.Enums.AccountLevel accountLevel)
        {
            return accountLevel switch
            {
                Storage.Enums.AccountLevel.Basic => AccountLevel.Basic,
                Storage.Enums.AccountLevel.Vip => AccountLevel.Vip,
                _ => throw new ArgumentOutOfRangeException(nameof(accountLevel), accountLevel, null)
            };
        }

        public static AccountStatus ToModel(this Storage.Enums.AccountStatus status)
        {
            return status switch
            {
                Storage.Enums.AccountStatus.Open => AccountStatus.Open,
                Storage.Enums.AccountStatus.Closed => AccountStatus.Closed,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        public static Currency ToModel(this Storage.Enums.Currency currency)
        {
            return currency switch
            {
                Storage.Enums.Currency.Eur => Currency.Eur,
                Storage.Enums.Currency.Usd => Currency.Usd,
                _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
            };
        } 
        
        public static Storage.Enums.AccountLevel ToEntity(this AccountLevel accountLevel)
        {
            return accountLevel switch
            {
                AccountLevel.Basic => Storage.Enums.AccountLevel.Basic,
                AccountLevel.Vip => Storage.Enums.AccountLevel.Vip,
                _ => throw new ArgumentOutOfRangeException(nameof(accountLevel), accountLevel, null)
            };
        }

        public static Storage.Enums.AccountStatus ToEntity(this AccountStatus status)
        {
            return status switch
            {
                AccountStatus.Open => Storage.Enums.AccountStatus.Open,
                AccountStatus.Closed => Storage.Enums.AccountStatus.Closed,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        public static Storage.Enums.Currency ToEntity(this Currency currency)
        {
            return currency switch
            {
                Currency.Eur => Storage.Enums.Currency.Eur,
                Currency.Usd => Storage.Enums.Currency.Usd,
                _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
            };
        }
    }
}