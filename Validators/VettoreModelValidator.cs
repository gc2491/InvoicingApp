using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel;

namespace AspNET.Validators
{
    public class VettoreModelValidator : AbstractValidator<CliForModel>
    {
        const string expectedErrorCode = "00200";

        public VettoreModelValidator()
        {
            RuleFor(x => x.IdPaese)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<IdPaese>())
                .WithName("IdPaese")
                .OverridePropertyName("idPaese");
            RuleFor(x => x.IdCodice)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 28)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("IdCodice")
                .OverridePropertyName("idCodice");
            RuleFor(x => x.CodiceFiscale)
                .Length(11, 16)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodiceFiscale")
                .OverridePropertyName("codiceFiscale");
            RuleFor(x => x.Denominazione)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.Nome) ||
                           string.IsNullOrEmpty(x.Cognome))
                .WithMessage("'{PropertyName}': il campo non può essere nullo se 'Nome' e 'Cognome' non sono definiti")
                .WithErrorCode(expectedErrorCode)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.Nome) ||
                           !string.IsNullOrEmpty(x.Cognome))
                .WithMessage("'{PropertyName}': il campo non può essere definito se 'Nome' e 'Cognome' non sono nulli")
                .WithErrorCode(expectedErrorCode)
                .Length(1, 80)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Denominazione")
                .OverridePropertyName("denominazione");
            RuleFor(x => x.Nome)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.Denominazione))
                .WithMessage("'{PropertyName}': il campo non può essere nullo se 'Denominazione' non è definito")
                .WithErrorCode(expectedErrorCode)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.Denominazione))
                .WithMessage("'{PropertyName}': il campo non può essere definito se 'Denominazione' non è nullo")
                .WithErrorCode(expectedErrorCode)
                .Length(1, 60)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .WithErrorCode(expectedErrorCode)
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Nome")
                .OverridePropertyName("nome");
            RuleFor(x => x.Cognome)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.Denominazione))
                .WithMessage("'{PropertyName}': il campo non può essere nullo se 'Denominazione' non è definito")
                .WithErrorCode(expectedErrorCode)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.Denominazione))
                .WithMessage("'{PropertyName}': il campo non può essere definito se 'Denominazione' non è nullo")
                .WithErrorCode(expectedErrorCode)
                .Length(1, 60)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Cognome")
                .OverridePropertyName("cognome");
            RuleFor(x => x.Titolo)
                .Length(2, 10)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Titolo")
                .OverridePropertyName("titolo");
            RuleFor(x => x.CodEORI)
                .Length(13, 17)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodEORI")
                .OverridePropertyName("codEORI");

            RuleFor(x => x.AlboProfessionale)
                .Length(1, 60)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("AlboProfessionale")
                .OverridePropertyName("alboProfessionale");
            RuleFor(x => x.ProvinciaAlbo)
                .Matches(@"^[A-Z]{2}$")
                .WithMessage("'{PropertyName}': il campo non è definito secodo lo standard")
                .WithName("ProvinciaAlbo")
                .OverridePropertyName("provinciaAlbo");
            RuleFor(x => x.NumeroIscrizioneAlbo)
                .Length(1, 60)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non consentiti")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("NumeroIscrizioneAlbo")
                .OverridePropertyName("numeroIscrizioneAlbo");

            RuleFor(x => x.NumeroLicenzaGuida)
                .Matches(@"^[a-zA-Z0-9]$")
                .WithMessage("'{PropertyName}': il campo accetta solo caratteri alfanumerici")
                .MaximumLength(20)
                .WithMessage("'{PropertyName}': il campo accetta massimo {MaxLength} caratteri. Ricevuti {TotalLength}")
                .WithName("NumeroIscrizioneAlbo")
                .OverridePropertyName("numeroIscrizioneAlbo");

            RuleForEach(x => x.Contatti)
                .SetValidator(new ContattiModelValidator())
                .WithName("Contatti")
                .OverridePropertyName("contatti");
            RuleForEach(x => x.ContiBancari)
                .SetValidator(new ContiBancariModelValidator())
                .WithName("ContiBancari")
                .OverridePropertyName("contiBancari");
            RuleForEach(x => x.Sedi)
                .SetValidator(new SediModelValidator())
                .WithName("Sedi")
                .OverridePropertyName("sedi");
        }
    }
}
