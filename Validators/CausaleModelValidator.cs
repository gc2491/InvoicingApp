using FluentValidation;
using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Validators
{
    public class CausaleModelValidator : AbstractValidator<CausaleModel>
    {
        public CausaleModelValidator()
        {
            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.Causale)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("'{PropertyName}': il campo può contenere massimo {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
        }
    }
}
