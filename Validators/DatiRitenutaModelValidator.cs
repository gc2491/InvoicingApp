using System.Text.RegularExpressions;
using AspNET.Models.InvoiceModel.Body;
using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;

namespace AspNET.Validators
{
    public class DatiRitenutaModelValidator : AbstractValidator<DatiRitenutaModel>
    {
        public DatiRitenutaModelValidator()
        {

            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.TipoRitenuta)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<TipoRitenuta>())
                .WithName("TipoRitenuta")
                .OverridePropertyName("tipoRitenuta");
            RuleFor(x => x.ImportoRitenuta)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImportoRitenuta")
                .OverridePropertyName("importoRitenuta");
            RuleFor(x => x.AliquotaRitenuta)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("Lungezza massima 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("AliquotaRitenuta")
                .OverridePropertyName("aliquotaRitenuta");
            RuleFor(x => x.CausalePagamento)
                .SetValidator(new IsValidValidator<CausalePagamento>())
                .OverridePropertyName("causalePagamento");
        }
    }
}
