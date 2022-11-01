using AccountsService.Storage.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountsService.Storage.Entities
{
    public class Operation : Entity
    {
        [EnumDataType(typeof(OperationType))]
        public OperationType Type { get; set; }
        public decimal Amount { get; set; }
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }

        [Required]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
