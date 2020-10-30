using FluentValidation;
using AspNET.Models.InvoiceModel.Header;

namespace AspNET.Validators
{
    public class ContattiModelValidator : AbstractValidator<ContattiModel>
    {
        public ContattiModelValidator()
        {
            RuleFor(x => x.CliforModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("CliforModelId")
                .OverridePropertyName("cliforModelId");
            RuleFor(x => x.Description)
                .MaximumLength(100)
                .WithMessage("'{PropertyName}': il campo può contenere massimo {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Description")
                .OverridePropertyName("description");
            RuleFor(x => x.Telefono)
                .Length(5, 12)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Telefono")
                .OverridePropertyName("telefono");
            RuleFor(x => x.Fax)
                .Length(5, 12)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Fax")
                .OverridePropertyName("fax");
            RuleFor(x => x.Email)
                .Length(7, 256)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .EmailAddress()
                .WithMessage("'{PropertyName}': il campo deve essere un indirizzo email valido")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Email")
                .OverridePropertyName("email");
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Telefono) ||
                           !string.IsNullOrEmpty(x.Fax) ||
                           !string.IsNullOrEmpty(x.Email))
                .WithMessage("Almeno un contatto deve essere definito.")
                .OverridePropertyName("generale");
            RuleFor(x => x.StillActive)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("StillActive")
                .OverridePropertyName("stillActive");
        }
    }
}
