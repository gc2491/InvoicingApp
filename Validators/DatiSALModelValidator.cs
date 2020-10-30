using FluentValidation;
using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Validators
{
    public class DatiSALModelValidator : AbstractValidator<DatiSALModel>
    {
        public DatiSALModelValidator()
        {
            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.RiferimentoFase)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x => x > 0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero")
                .WithName("RiferimentoFase")
                .OverridePropertyName("riferimentoFase");
        }
    }
}
