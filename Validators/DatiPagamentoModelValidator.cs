using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Validators
{
    public class DatiPagamentoModelValidator : AbstractValidator<DatiPagamentoModel>
    {
        public DatiPagamentoModelValidator()
        {
            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.CondizioniPagamento)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<CondizioniPagamento>())
                .WithName("CondizioniPagamento")
                .OverridePropertyName("condizioniPagamento");
            RuleForEach(x => x.DettaglioPagamento)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new DettaglioPagamentoModelValidator())
                .WithName("DettaglioPagamento")
                .OverridePropertyName("dettaglioPagamento");
        }
    }
}
