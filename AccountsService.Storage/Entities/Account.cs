using AccountsService.Storage.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountsService.Storage.Entities
{
    public class Account : Entity
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        [EnumDataType(typeof(AccountStatus))]
        public AccountStatus Status { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        [EnumDataType(typeof(AccountLevel))]
        public AccountLevel Level { get; set; }
        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }

        public List<Operation> Operations { get; set; }
    }
}