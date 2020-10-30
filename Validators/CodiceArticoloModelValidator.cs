using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body.Linee;

namespace AspNET.Validators
{
    public class CodiceArticoloModelValidator : AbstractValidator<CodiceArticoloModel>
    {
        public CodiceArticoloModelValidator()
        {
            RuleFor(x => x.DettaglioLineeId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("DettaglioLineeId")
                .OverridePropertyName("dettaglioLineeId");
            RuleFor(x => x.CodiceTipo)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 35)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodiceTipo")
                .OverridePropertyName("codiceTipo");
            RuleFor(x => x.CodiceValore)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 35)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodiceValore")
                .OverridePropertyName("codiceValore");
        }
    }
}
