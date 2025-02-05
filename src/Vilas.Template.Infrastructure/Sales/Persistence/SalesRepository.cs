using Microsoft.EntityFrameworkCore;
using Vilas.Template.Application.Common.Interfaces.Repository;
using Vilas.Template.Domain.Sales;
using Vilas.Template.Infrastructure.Common.Persistence;

namespace Vilas.Template.Infrastructure.Sales.Persistence
{
    public class SalesRepository(AppDbContext _dbContext) : ISalesRepository
    {
        public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(sale, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Sale?> GetByIdAsync(Guid saleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Sales
                   .Include(x => x.Items)
                   .Where(x => x.Id == saleId)
                   .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            _dbContext.Update(sale);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
