using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel;

namespace AspNET.Validators
{
    public class MetadataModelValidator : AbstractValidator<MetadataModel>
    {
        public MetadataModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("Id")
                .OverridePropertyName("id");
            RuleFor(x => x.TrasmittenteId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("TrasmittenteId")
                .OverridePropertyName("trasmittenteId");
            RuleFor(x => x.IntermediarioOEmittenteId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("IntermediarioOEmittenteId")
                .OverridePropertyName("intermediarioOEmittenteId");
            RuleFor(dt => dt.ProgressivoInvio)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 10)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("IntermediarioOEmittenteId")
                .OverridePropertyName("intermediarioOEmittenteId");
            RuleFor(dt => dt.FormatoTrasmissione)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<FormatoTrasmissione>())
                .WithErrorCode("00428")
                .WithName("FormatoTrasmissione")
                .OverridePropertyName("formatoTrasmissione");
            RuleFor(dt => dt.CodiceDestinatario)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(6)
                .When(dt => dt.FormatoTrasmissione == FatturaElettronica.Defaults.FormatoTrasmissione.PubblicaAmministrazione)
                .WithErrorCode("00427")
                .Length(7)
                .When(dt => dt.FormatoTrasmissione == FatturaElettronica.Defaults.FormatoTrasmissione.Privati)
                .WithErrorCode("00427")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("FormatoTrasmissione")
                .OverridePropertyName("formatoTrasmissione");
            RuleFor(x => x.SoggettoEmittente)
                .SetValidator(new IsValidValidator<SoggettoEmittente>())
                .WithName("SoggettoEmittente")
                .OverridePropertyName("soggettoEmittente");
            RuleFor(dt => dt.PECDestinatario)
                .Length(7, 256)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .EmailAddress()
                .WithMessage("'{PropertyName}': il campo deve essere un indirizzo email valido")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("PECDestinatario")
                .OverridePropertyName("pecDestinatario");
        }
    }
}
