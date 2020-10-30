using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Header;

namespace AspNET.Validators
{
    public class ContiBancariModelValidator : AbstractValidator<ContiBancariModel>
    {
        public ContiBancariModelValidator()
        {
            RuleFor(x => x.CliforModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("CliforModelId")
                .OverridePropertyName("cliforModelId");
            RuleFor(x => x.IBAN)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Matches(@"[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{7,26}")
                .WithMessage("'{PropertyName}': il valore inserito non è un codice IBAN valido")
                .Length(11,30)
                .WithMessage("'{PropertyName}': valore inserito non valido. Lunghezza deve essere tra {MinLength} e {MaxLength} caratteri. Ricevuto {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("IBAN")
                .OverridePropertyName("iban");
            RuleFor(x => x.ABI)
                .Length(5,5)
                .WithMessage("'{PropertyName}': valore inserito non valido. Lunghezza deve essere pari a {MaxLength} caratteri. Ricevuto {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("ABI")
                .OverridePropertyName("abi");
            RuleFor(x => x.CAB)
                .Length(5,5)
                .WithMessage("'{PropertyName}': valore inserito non valido. Lunghezza deve essere pari a {MaxLength} caratteri. Ricevuto {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CAB")
                .OverridePropertyName("cab");
            RuleFor(x => x.BIC)
                .Length(8, 11)
                .WithMessage("'{PropertyName}': valore inserito non valido. Lunghezza deve essere tra {MinLength} e {MaxLength} caratteri. Ricevuto {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("BIC")
                .OverridePropertyName("bic");
            RuleFor(x => x.IstitutoFinanziario)
                .Length(1, 80)
                .WithMessage("'{PropertyName}': valore inserito non valido. Lunghezza deve essere tra {MinLength} e {MaxLength} caratteri. Ricevuto {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("IstitutoFinanziario")
                .OverridePropertyName("istitutoFinanziario");
            RuleFor(x => x.StillActive)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("StillActive")
                .OverridePropertyName("stillActive");
        }
    }
}
