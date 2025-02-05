using ErrorOr;
using MediatR;
using Vilas.Template.Application.Common.Interfaces.Repository;
using Vilas.Template.Domain.Sales;
using Vilas.Template.Domain.Sells;

namespace Vilas.Template.Application.Sales.Commands.AddItemSale;

public class AddItemSaleCommnadHandler(ISalesRepository _salesRepository)
    : IRequestHandler<AddItemSaleCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddItemSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _salesRepository.GetByIdAsync(request.SaleId, cancellationToken);

        if (sale == null)
            return Error.NotFound(description: "Não foi encontrada uma venda com esse identificador");

        if (sale.Status != SaleStatus.WaitingPayment)
            return Error.Conflict(description: "Só é possível adicionar itens enquanto a venda está no status de aguardando pagamento");

        foreach (var item in request.Items)
            sale.AddItem(new ItemSale(Guid.NewGuid(), sale.Id, item.Name, item.Price, item.Quantity));

        await _salesRepository.UpdateAsync(sale, cancellationToken);

        return Result.Success;
    }
}
