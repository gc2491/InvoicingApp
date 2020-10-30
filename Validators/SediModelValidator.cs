using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Header;

namespace AspNET.Validators
{
    public class SediModelValidator : AbstractValidator<SediModel>
    {
        public SediModelValidator()
        {
            RuleFor(x => x.CliforModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("CliforModelId")
                .OverridePropertyName("cliforModelId");
            RuleFor(x => x.Description)
                .MaximumLength(100)
                .WithMessage("'{PropertyName}': il campo può contenere massimo {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Description")
                .OverridePropertyName("description");
            RuleFor(x => x.Indirizzo)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo è obbligatorio")
                .Length(1, 60)
                .WithMessage("{PropertyName}: il campo può contenere massimo 60 caratteri. Ricevuti: {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Indirizzo")
                .OverridePropertyName("indirizzo");
            RuleFor(x => x.NumeroCivico)
                .Length(1, 8)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("NumeroCivico")
                .OverridePropertyName("numeroCivico");
            RuleFor(x => x.CAP)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo è obbligatorio")
                .Length(5,5)
                .WithMessage("'{PropertyName}': il campo deve contenere {MinLength} caratteri. Ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CAP")
                .OverridePropertyName("cap");
            RuleFor(x => x.Comune)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo è obbligatorio")
                .Length(1, 60)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} and {MaxLength}. Ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Comune")
                .OverridePropertyName("comune");
            RuleFor(x => x.Provincia)
                .Matches(@"^[A-Z]{2}$")
                .WithMessage("'{PropertyName}': campo non specificato secondo lo standard")
                .WithName("Provincia")
                .OverridePropertyName("provincia");
            RuleFor(x => x.Nazione)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo è obbligatorio")
                .SetValidator(new IsValidValidator<IdPaese>())
                .WithMessage("'{PropertyName}': campo non specificato secondo lo standard")
                .WithName("Nazione")
                .OverridePropertyName("nazione");
            RuleFor(x => x.StillActive)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("StillActive")
                .OverridePropertyName("stillActive");
        }
    }
}
