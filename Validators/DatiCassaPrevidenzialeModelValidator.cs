using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body;
using System.Threading;
using System.Text.RegularExpressions;

namespace AspNET.Validators
{
    public class DatiCassaPrevidenzialeModelValidator : AbstractValidator<DatiCassaPrevidenzialeModel>
    {
        public DatiCassaPrevidenzialeModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.TipoCassa)
                .SetValidator(new IsValidValidator<TipoCassa>())
                .WithName("TipoCassa")
                .OverridePropertyName("tipoCassa");
            RuleFor(x => x.AlCassa)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("AlCassa")
                .OverridePropertyName("alCassa");
            RuleFor(x => x.ImportoContributoCassa)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImportoContributoCassa")
                .OverridePropertyName("importoContributoCassa");
            RuleFor(x => x.ImponibileCassa)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImponibileCassa")
                .OverridePropertyName("imponibileCassa");
            RuleFor(x => x.AliquotaIVA)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("AliquotaIVA")
                .OverridePropertyName("aliquotaIVA");
            RuleFor(x => x.Ritenuta)
                .Equal("SI")
                .WithMessage("'{PropertyName}': il campo accetta solo il valore 'SI'. Ricevuto '{PropertyValue}'")
                .WithName("Ritenuta")
                .OverridePropertyName("ritenuta");
            RuleFor(x => x.Natura)
                .SetValidator(new IsValidValidator<Natura>())
                .OverridePropertyName("Natura");
            RuleFor(x => x.Natura)
                .Empty()
                .When(x => x.AliquotaIVA != 0)
                .WithMessage("'{PropertyName}': il campo deve essere nullo se 'AliquotaIVA' è diverso da zero")
                .WithErrorCode("00414")
                .WithName("Natura")
                .OverridePropertyName("natura");
            RuleFor(x => x.RiferimentoAmministrazione)
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("RiferimentoAmministrazione")
                .OverridePropertyName("riferimentoAmministrazione");
        }
    }
}
