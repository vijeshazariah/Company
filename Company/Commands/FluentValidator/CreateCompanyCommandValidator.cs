using FluentValidation;


namespace Company.Commands.FluentValidator
{
    public class CreateCompanyCommandValidator: AbstractValidator<AddCompany.CompanyCommand>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(255).WithMessage("Company name must not exceed 255 characters.");

            RuleFor(c => c.StockTicker)
                .NotEmpty().WithMessage("Stock Ticker is required.")
                .MaximumLength(10).WithMessage("Stock Ticker must not exceed 10 characters.");

            RuleFor(c => c.Exchange)
                .NotEmpty().WithMessage("Exchange is required.")
                .MaximumLength(100).WithMessage("Exchange must not exceed 100 characters.");

            RuleFor(c => c.Isin)
                .NotEmpty().WithMessage("ISIN is required.")
                .Length(12).WithMessage("ISIN must be exactly 12 characters.")
                .Matches(@"^[A-Za-z]{2}\w{10}$").WithMessage("ISIN must start with two letters followed by 10 alphanumeric characters.");

            
        }
    }
}
