using AccountsService.Core.Exceptions;
using AccountsService.Core.Repositories.Cashbacks;
using AccountsService.Core.Repositories.Operations;
using AccountsService.Core.Services.Accounts;
using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;
using AccountsService.Models.Operations;

namespace AccountsService.Core.Services.Operations
{
    public class OperationsService : IOperationsService
    {
        private readonly IAccountsService _accountService;
        private readonly ICashbacksRepository _cashbacksRepository;
        private readonly IOperationsRepository _operationsRepository;

        public OperationsService(IAccountsService accountService,
            ICashbacksRepository cashbacksRepository,
            IOperationsRepository operationsRepository)
        {
            _accountService = accountService;
            _cashbacksRepository = cashbacksRepository;
            _operationsRepository = operationsRepository;
        }

        public async Task<AccountModel> ExecuteOperation(int accountNumber, OperationModel operationModel, CancellationToken cancellationToken)
        {
            var account = await _accountService.ValidateAndGetAccountForOperation(accountNumber, operationModel, cancellationToken);

            account.Balance = operationModel.Type switch
            {
                OperationType.Deposit => PerformDepositOperation(account, operationModel),
                OperationType.Payment => await PerformPaymentOperation(account, operationModel, cancellationToken),
                _ => throw new DomainException(DomainErrorCode.InvalidArgument,
                    $"Operation type: {operationModel.Type} is invalid")
            };

            await _operationsRepository.Save(account.Id, operationModel, cancellationToken);
            await _accountService.Update(account, cancellationToken);

            return account;
        }

        private async Task<decimal> PerformPaymentOperation(AccountModel account, OperationModel operationModel, CancellationToken cancellationToken)
        {
            if (account.Balance < operationModel.Amount)
                throw new DomainException(DomainErrorCode.InvalidOperation, "Account balance is insufficient");

            var cashback = await CountCashback(operationModel.Amount, account.Level, cancellationToken);

            return account.Balance - operationModel.Amount + cashback;
        }

        private async Task<decimal> CountCashback(decimal amount, AccountLevel accountLevel, CancellationToken cancellationToken)
        {
            var percentage = await _cashbacksRepository.GetCashbackPercentageByAccountLevel(accountLevel, cancellationToken);
            return amount * percentage / 100;
        }

        private decimal PerformDepositOperation(AccountModel account, OperationModel operationModel)
        {
            return account.Balance + operationModel.Amount;
        }
    }
}
