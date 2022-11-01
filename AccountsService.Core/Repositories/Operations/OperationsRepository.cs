using AccountsService.Core.Extensions.Models;
using AccountsService.Models.Operations;
using AccountsService.Storage.Data;

namespace AccountsService.Core.Repositories.Operations
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Save(Guid accountNumber, OperationModel operationModel, CancellationToken cancellationToken)
        {
            await _context.Operations.AddAsync(operationModel.ToEntity(accountNumber), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}