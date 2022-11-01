using AccountsService.Core.Exceptions;
using AccountsService.Core.Repositories.Cashbacks;
using AccountsService.Core.Repositories.Operations;
using AccountsService.Core.Services.Accounts;
using AccountsService.Core.Services.Operations;
using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;
using AccountsService.Models.Operations;
using AccountsService.Tests.Infrastructure;
using Moq;

namespace AccountsService.Tests
{
    public class OperationsServiceTests
    {
        private readonly Mock<ICashbacksRepository> _cashbackRepositoryMock;
        private readonly Mock<IAccountsService> _accountServiceMock;
        private readonly Mock<IOperationsRepository> _operationsRepositoryMock;
        private readonly CancellationToken _cancellationToken;

        public OperationsServiceTests()
        {
            _cashbackRepositoryMock = new Mock<ICashbacksRepository>();
            _accountServiceMock = new Mock<IAccountsService>();
            _operationsRepositoryMock = new Mock<IOperationsRepository>();
            _cancellationToken = new CancellationTokenSource().Token;
        }

        [Fact]
        public async Task ExecuteOperation_BalanceIsSmallerThanPayment_ShouldThrowException()
        {
            var accountBalance = 0;
            var paymentAmount = 200;

            var operationModel = DataHelper.GetOperationModel(paymentAmount, OperationType.Payment);
            var account = DataHelper.GetAccount(AccountLevel.Vip, accountBalance);
            SetupAccountValidation(operationModel, account);

            var operationsService = GetOperationService();

            var domainException = await Assert.ThrowsAsync<DomainException>(() => operationsService.ExecuteOperation(account.AccountNumber,
                operationModel, _cancellationToken));

            Assert.Equal(DomainErrorCode.InvalidOperation, domainException.ErrorCode);
        }

        [Fact]
        public async Task ExecuteOperation_CashbackConfigured_ShouldSubtractPaymentAndAddChashBack()
        {
            var accountBalance = 300;
            var paymentAmount = 200;
            await AssertOperation(accountBalance, paymentAmount, OperationType.Payment, 1);
        }
        
        [Fact]
        public async Task ExecuteOperation_AccountIsValid_ShouldAddDeposit()
        {
            var accountBalance = 300;
            var depositAmount = 200;
            await AssertOperation(accountBalance, depositAmount, OperationType.Deposit);
        }

        private async Task AssertOperation(decimal accountBalance, decimal amount, OperationType operationType, decimal cashbackPercentage = 0)
        {
            var operationModel = DataHelper.GetOperationModel(amount, operationType);
            var account = DataHelper.GetAccount(AccountLevel.Vip, accountBalance);

            SetupAccountValidation(operationModel, account);
            SetupCashbackPercentage(account.Level, cashbackPercentage);

            var operationService = GetOperationService();
            var actualAccount = await operationService.ExecuteOperation(account.AccountNumber, operationModel, _cancellationToken);

            Assert.Equal(GetExpectedBalance(operationType, accountBalance, amount, 1), actualAccount.Balance);
        }

        private decimal GetExpectedBalance(OperationType operationType, decimal accountBalance, decimal amount, decimal percentage)
        {
            return operationType switch
            {
                OperationType.Deposit => accountBalance + amount,
                OperationType.Payment => accountBalance - amount + (amount * percentage / 100),
                _ => throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null),
            };
        }

        private void SetupCashbackPercentage(AccountLevel expectedAccountLevel, decimal percentage = 0)
        {
            _cashbackRepositoryMock.Setup(x =>
                x.GetCashbackPercentageByAccountLevel(expectedAccountLevel, _cancellationToken))
                .ReturnsAsync(percentage);
        }
        
        private OperationsService GetOperationService() => new OperationsService(_accountServiceMock.Object,
            _cashbackRepositoryMock.Object, _operationsRepositoryMock.Object);

        private void SetupAccountValidation(OperationModel operationModel, AccountModel accountModel)
        {
            _accountServiceMock.Setup(x =>
                    x.ValidateAndGetAccountForOperation(accountModel.AccountNumber, operationModel, _cancellationToken))
                .ReturnsAsync(accountModel);
        }
    }
}
