using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body.Linee;
using System.Threading;

namespace AspNET.Validators
{
    public class AltriDatiGestionaliModelValidator : AbstractValidator<AltriDatiGestionaliModel>
    {
        public AltriDatiGestionaliModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.DettaglioLineeModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("DettaglioLineeModelId")
                .OverridePropertyName("dettaglioLineeModelId");
            RuleFor(x => x.TipoDato)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Length(1, 10)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("TipoDato")
                .OverridePropertyName("tipoDato");
            RuleFor(x => x.RiferimentoTesto)
                .Length(1, 60)
                .Latin1SupplementValidator()
                .WithName("RiferimentoTesto")
                .WithMessage("Lungezza massima 21 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("RiferimentoTesto")
                .OverridePropertyName("riferimentoTesto");
            RuleFor(x => x.RiferimentoNumero.ToString())
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Matches(@"^\d{1,18}\.\d{2}$")
                .WithMessage("'{PropertyName}': il campo può avere massime 21 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("RiferimentoNumero")
                .OverridePropertyName("tipoDato");
        }
    }
}
