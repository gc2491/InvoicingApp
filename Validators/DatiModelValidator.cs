using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Customizations.FatturaElettronicaNuGet.Tabelle;

using AspNET.Models.InvoiceModel.Body;

namespace AspNET.Validators
{
    public class DatiModelValidator : AbstractValidator<DatiModel>
    {
        public DatiModelValidator()
        {
            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.DataType)
                .SetValidator(new IsValidValidator<DataType>())
                .WithName("DataType")
                .OverridePropertyName("dataType");
            RuleFor(x => x.IdDocumento)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("IdDocumento")
                .OverridePropertyName("idDocumento");
            RuleFor(x => x.NumItem)
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("NumItem")
                .OverridePropertyName("numItem");
            RuleFor(x => x.CodiceCommessaConvenzione)
                .Length(1, 100)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodiceCommessaConvenzione")
                .OverridePropertyName("codiceCommessaConvenzione");
            RuleFor(x => x.CodiceCUP)
                .Length(1, 15)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodiceCUP")
                .OverridePropertyName("codiceCUP");
            RuleFor(x => x.CodiceCIG)
                .Length(1, 15)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodiceCIG")
                .OverridePropertyName("codiceCIG");
            RuleForEach(x => x.RiferimentoNumeroLinea)
                .SetValidator(new RiferimentoNumeroLineaModelValidator())
                .OverridePropertyName("riferimentoNumeroLinea");
        }
    }
}
