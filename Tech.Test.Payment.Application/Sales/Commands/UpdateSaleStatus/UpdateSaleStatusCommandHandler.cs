using ErrorOr;
using MediatR;
using Tech.Test.Payment.Application.Common.Interfaces.Repository;

namespace Tech.Test.Payment.Application.Sales.Commands.UpdateSaleStatus
{
    public class UpdateSaleStatusCommandHandler(ISalesRepository _salesRepository) : IRequestHandler<UpdateSaleStatusCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(UpdateSaleStatusCommand request, CancellationToken cancellationToken)
        {
            var sale = await _salesRepository.GetByIdAsync(request.SaleId,cancellationToken);

            if (sale == null)
                return Error.NotFound(description: "Não foi encontrada uma venda com esse identificador");

            var result = sale.UpdateStatus(request.NewStatus);

            if (result.IsError)
                return result;

            await _salesRepository.UpdateAsync(sale,cancellationToken);

            return result;
        }
    }
}
