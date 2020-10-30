using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body;
using System.Threading;
using System.Text.RegularExpressions;

namespace AspNET.Validators
{
    public class DatiRiepilogoModelValidator : AbstractValidator<DatiRiepilogoModel>
    {
        public DatiRiepilogoModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.AliquotaIVA)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("Lungezza massima 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("AliquotaIVA")
                .OverridePropertyName("aliquotaIVA");
            RuleFor(x => x.Natura)
                .SetValidator(new IsValidValidator<Natura>())
                .OverridePropertyName("natura");
            RuleFor(x => x.SpeseAccessorie)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("SpeseAccessorie")
                .OverridePropertyName("speseAccessorie");
            RuleFor(x => x.Arrotondamento)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^-{0,1}\d{1,12}\.\d{8}$");

                    return result.Success;
                })
                .When(x => x.Arrotondamento != null)
                .WithMessage("Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Arrotondamento")
                .OverridePropertyName("arrotondamento");
            RuleFor(x => x.ImponibileImporto)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImponibileImporto")
                .OverridePropertyName("imponibileImporto");
            RuleFor(x => x.Imposta)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Imposta")
                .OverridePropertyName("imposta");
            RuleFor(x => x.EsigibilitaIVA)
                .SetValidator(new IsValidValidator<EsigibilitaIVA>())
                .OverridePropertyName("esigibilitaIVA");
            RuleFor(x => x.RiferimentoNormativo)
                .Length(1, 100)
                .WithMessage("'{PropertyName}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("RiferimentoNormativo")
                .OverridePropertyName("riferimentoNormativo");
        }
    }
}
