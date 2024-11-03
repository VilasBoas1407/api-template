using Tech.Test.Payment.Application.Common.Interfaces.Repository;
using Tech.Test.Payment.Domain.Sales;
using Tech.Test.Payment.Infrastructure.Common.Persistence;

namespace Tech.Test.Payment.Infrastructure.Sales.Persistence
{
    public class SalesRepository(AppDbContext _dbContext) : ISalesRepository
    {
        public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(sale, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
