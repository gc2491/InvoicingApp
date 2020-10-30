using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body.Linee;
using System.Threading;
using System.Text.RegularExpressions;

namespace AspNET.Validators
{
    public class ScontoMaggiorazioneModelValidator : AbstractValidator<ScontoMaggiorazioneModel>
    {
        public ScontoMaggiorazioneModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("Id")
                .OverridePropertyName("id");
            RuleFor(x => x.DettaglioLineeId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("DettaglioLineeId")
                .OverridePropertyName("dettaglioLineeId");
            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must((x, _) => x.Importo != null || x.Percentuale != null)
                .WithMessage("Percentuale e Importo non presenti a fronte di Tipo valorizzato")
                .WithErrorCode("00437")
                .SetValidator(new IsValidValidator<ScontoMaggiorazione>())
                .WithName("Tipo")
                .OverridePropertyName("tipo");
            RuleFor(x => x.Percentuale)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Percentuale")
                .OverridePropertyName("percentuale");
            RuleFor(x => x.Importo)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Importo")
                .OverridePropertyName("importo");
        }
    }
}
