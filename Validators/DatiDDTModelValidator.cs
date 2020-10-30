using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Validators
{
    public class DatiDDTModelValidator : AbstractValidator<DatiDDTModel>
    {
        public DatiDDTModelValidator()
        {
            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.DataDDT)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("DataDDT")
                .OverridePropertyName("dataDDT");
            RuleFor(x => x.NumeroDDT)
                .NotEmpty()
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Matches(@"\d")
                .WithMessage("Numero non contiene caratteri numerici")
                .WithErrorCode("00425")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("NumeroDDT")
                .OverridePropertyName("numeroDDT");
        }
    }
}
