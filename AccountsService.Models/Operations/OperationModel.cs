using AccountsService.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Operations
{
    public class OperationModel
    {
        [EnumDataType(typeof(OperationType))]
        public OperationType Type { get; set; }
        [Range(0, 99999999999999999)]
        public decimal Amount { get; set; }
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
    }
}