using FluentValidation;
using AspNET.Models.InputModels;
using System.Threading;

namespace AspNET.Validators
{
    public class ProductReportInputModelValidator : AbstractValidator<ProductReportInputModel>
    {
        public ProductReportInputModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.Products)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x => string.IsNullOrEmpty(x) ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contenere i caratteri '<' e '>'")
                .WithName("Products")
                .OverridePropertyName("products");
        }
    }
}
