using System.ComponentModel.DataAnnotations;
using AccountsService.Storage.Enums;

namespace AccountsService.Storage.Entities
{
    public class CashbackConfiguration : Entity
    {
        [Required]
        [EnumDataType(typeof(AccountLevel))]
        public AccountLevel AccountLevel { get; set; }
        [Required]
        public decimal CashbackPercentange { get; set; }
    }
}
