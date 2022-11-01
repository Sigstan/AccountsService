using AccountsService.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Accounts
{
    public class AccountModel : AccountBaseModel
    {
        public decimal Balance { get; set; }
        [EnumDataType(typeof(AccountLevel))]
        public AccountLevel Level { get; set; }
        [EnumDataType(typeof(AccountStatus))]
        public AccountStatus Status { get; set; }
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }
    }
}