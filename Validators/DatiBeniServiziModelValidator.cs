using System.Text.RegularExpressions;
using AspNET.Models.InvoiceModel.Body;
using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;

namespace AspNET.Validators
{
    public class DatiBeniServiziModelValidator : AbstractValidator<DatiBeniServiziModel>
    {
        public DatiBeniServiziModelValidator()
        {
            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo è obbligatorio")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.Descrizione)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo è obbligatorio")
                .Length(1,1000)
                .WithMessage("'{PropertyName}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Descrizione")
                .OverridePropertyName("descrizione");
            RuleFor(x => x.Importo)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Importo")
                .OverridePropertyName("importo");
            RuleFor(x => x.Imposta)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Imposta")
                .OverridePropertyName("imposta");
            RuleFor(x => x.Aliquota)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': il campo può contenere massimo 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Aliquota")
                .OverridePropertyName("aliquota");
            RuleFor(x => x.Natura)
                .SetValidator(new IsValidValidator<Natura>())
                .WithName("Natura")
                .OverridePropertyName("natura");
            RuleFor(x => x.RiferimentoNormativo)
                .Length(1,1000)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("RiferimentoNormativo")
                .OverridePropertyName("riferimentoNormativo");
        }
    }
}
