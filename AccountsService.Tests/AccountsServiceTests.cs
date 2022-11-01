using AccountsService.Core.Exceptions;
using AccountsService.Core.Repositories.Accounts;
using AccountsService.Models.Accounts;
using AccountsService.Models.Enums;
using AccountsService.Tests.Infrastructure;
using Moq;

namespace AccountsService.Tests
{
    public class AccountsServiceTests
    {
        private readonly Mock<IAccountsRepository> _accountsRepository;
        private readonly CancellationToken _cancellationToken;

        public AccountsServiceTests()
        {
            _accountsRepository = new Mock<IAccountsRepository>();
            _cancellationToken = new CancellationTokenSource().Token;
        }

        [Fact]
        public async Task ValidateAndGetAccountForOperation_InvalidCurrency_ShouldThrowException()
        {
            await AssertValidateAndGetAccountForOperationMethod(Currency.Usd, AccountStatus.Open, DomainErrorCode.InvalidCurrency);
           
        }
        
        [Fact]
        public async Task ValidateAndGetAccountForOperation_AccountClosed_ShouldThrowException()
        {
            await AssertValidateAndGetAccountForOperationMethod(Currency.Eur, AccountStatus.Closed, DomainErrorCode.InvalidOperation);
           
        }

        private async Task AssertValidateAndGetAccountForOperationMethod(Currency currency, AccountStatus accountStatus, DomainErrorCode errorCode)
        {
            var operationModel = DataHelper.GetOperationModel(200, OperationType.Payment, currency);
            var accountModel = DataHelper.GetAccount(AccountLevel.Vip, 300, accountStatus);

            SetupAccountsRepository(accountModel);

            var accountService = GetAccountService();

            var domainException = await Assert.ThrowsAsync<DomainException>(() => accountService.ValidateAndGetAccountForOperation(accountModel.AccountNumber,
                operationModel, _cancellationToken));

            Assert.Equal(errorCode, domainException.ErrorCode);
        }

        private void SetupAccountsRepository(AccountModel accountModel)
        {
            _accountsRepository.Setup(x => x.GetAccountByAccountNumber(accountModel.AccountNumber, _cancellationToken))
                .ReturnsAsync(accountModel);
        }

        private Core.Services.Accounts.AccountsService GetAccountService() => 
            new Core.Services.Accounts.AccountsService(_accountsRepository.Object);
    }
}
