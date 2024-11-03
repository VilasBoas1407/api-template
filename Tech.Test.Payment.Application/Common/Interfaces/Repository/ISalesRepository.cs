using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Application.Common.Interfaces.Repository;

public interface ISalesRepository
{
    Task AddAsync(Sale sale, CancellationToken cancellationToken);
    Task<Sale?> GetByIdAsync(Guid saleId, CancellationToken cancellationToken);
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken);
}
