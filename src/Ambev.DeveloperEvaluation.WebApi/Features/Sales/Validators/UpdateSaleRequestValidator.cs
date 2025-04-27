using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one item must be provided.");
        }
    }
}
