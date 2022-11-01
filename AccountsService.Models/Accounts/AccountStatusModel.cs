using AccountsService.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Accounts
{
    public class AccountStatusModel : AccountBaseModel
    {
        [EnumDataType(typeof(AccountStatus))]
        public AccountStatus Status { get; set; }
    }
}