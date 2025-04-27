using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
            RuleFor(x => x.Customer).NotEmpty();
            RuleFor(x => x.Branch).NotEmpty();
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one item must be provided.");
        }
    }
}
