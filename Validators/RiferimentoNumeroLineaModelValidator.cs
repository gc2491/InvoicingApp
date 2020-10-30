using FluentValidation;
using AspNET.Models.InvoiceModel.Body.RiferimentoNumeroLinea;

namespace AspNET.Validators
{
    public class RiferimentoNumeroLineaModelValidator : AbstractValidator<RiferimentoNumeroLineaModel>
    {
        public RiferimentoNumeroLineaModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("Id")
                .OverridePropertyName("id");
            RuleFor(x => x.DatiModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("DatiModelId")
                .OverridePropertyName("datiModelId");
            RuleFor(x => x.RiferimentoNumeroLinea)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .GreaterThan(0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("RiferimentoNumeroLinea")
                .OverridePropertyName("riferimentoNumeroLinea");
        }
    }
}
