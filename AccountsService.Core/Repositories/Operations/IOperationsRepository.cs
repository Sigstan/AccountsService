using AccountsService.Models.Operations;

namespace AccountsService.Core.Repositories.Operations
{
    public interface IOperationsRepository
    {
        Task Save(Guid accountNumber, OperationModel operationModel, CancellationToken cancellationToken);
    }
}