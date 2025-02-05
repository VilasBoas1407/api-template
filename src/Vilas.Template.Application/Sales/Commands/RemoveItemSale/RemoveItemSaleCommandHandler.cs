using ErrorOr;
using MediatR;
using Vilas.Template.Application.Common.Interfaces.Repository;
using Vilas.Template.Domain.Sells;

namespace Vilas.Template.Application.Sales.Commands.RemoveItemSale;

public class RemoveItemSaleCommandHandler(ISalesRepository _salesRepository) : IRequestHandler<RemoveItemSaleCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RemoveItemSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _salesRepository.GetByIdAsync(request.SaleId, cancellationToken);

        if (sale == null)
            return Error.NotFound(description: "Não foi encontrada uma venda com esse identificador");

        if (sale.Status != SaleStatus.WaitingPayment)
            return Error.Conflict(description: "Só é possível adicionar itens enquanto a venda está no status de aguardando pagamento");

        foreach (var itemId in request.IdsItemsSales)
        {
            var result = sale.RemoveItem(itemId);

            if (result.IsError)
                return result;
        }

        await _salesRepository.UpdateAsync(sale, cancellationToken);

        return Result.Success;
    }
}
