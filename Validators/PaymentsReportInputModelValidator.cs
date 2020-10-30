using FluentValidation;
using AspNET.Models.InputModels;
using System.Threading;
using FatturaElettronica.Validators;

namespace AspNET.Validators
{
    public class PaymentsReportInputModelValidator : AbstractValidator<PaymentsReportInputModel>
    {
        public PaymentsReportInputModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.Clifor)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 80)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Clifor")
                .OverridePropertyName("clifor");
            RuleFor(x => x.CliforId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero. Ricevuto {PropertyValue}")
                .WithName("CliforId")
                .OverridePropertyName("clifor");
        }
    }
}
