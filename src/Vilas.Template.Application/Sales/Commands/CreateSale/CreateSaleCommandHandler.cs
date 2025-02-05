using ErrorOr;
using MediatR;
using Vilas.Template.Application.Common.Interfaces.Repository;
using Vilas.Template.Application.Common.Security.CurrentUserProvider;
using Vilas.Template.Domain.Sales;

namespace Vilas.Template.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandHandler(ICurrentUserProvider _userContextService,
    ISalesRepository _salesRepository) : IRequestHandler<CreateSaleCommand, ErrorOr<Sale>>
{
    public async Task<ErrorOr<Sale>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request.Items.Count == 0)
            return Error.Validation(code: "Validation", description: "A venda tem que ter pelo menos um item.");

        var user = _userContextService.GetCurrentUser();

        var sale = new Sale(Guid.NewGuid(), request.CustomerName, request.CustomerPhone,
            user.Id, user.Name, user.Cpf, user.Email, user.Phone);

        foreach (var item in request.Items)
            sale.AddItem(new ItemSale(Guid.NewGuid(), sale.Id,
                item.Name, item.Price, item.Quantity));


        await _salesRepository.AddAsync(sale, cancellationToken);

        return sale;
    }
}
