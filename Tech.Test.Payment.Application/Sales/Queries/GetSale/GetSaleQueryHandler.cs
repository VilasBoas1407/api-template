using ErrorOr;
using MediatR;
using Tech.Test.Payment.Application.Common.Interfaces.Repository;
using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Application.Sales.Queries.GetSale;

public class GetSaleQueryHandler(ISalesRepository _salesRepository) : IRequestHandler<GetSaleQuery, ErrorOr<Sale>>
{
    public async Task<ErrorOr<Sale>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var sale = await _salesRepository.GetByIdAsync(request.SaleId, cancellationToken);

        if (sale == null)
            return Error.NotFound(description: "Não foi encontrada uma venda.");

        return sale;
    }
}
