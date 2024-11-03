using FluentValidation;

namespace Tech.Test.Payment.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .MinimumLength(2)
            .MaximumLength(1000)
            .NotEmpty();

        RuleFor(x => x.CustomerPhone)
            .MinimumLength(2)
            .MaximumLength(13)
            .NotEmpty();

    }
}
