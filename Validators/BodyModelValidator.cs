using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace AspNET.Validators
{
    public class BodyModelValidator : AbstractValidator<BodyModel>
    {
        public BodyModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.CedentePrestatoreId)
                .Must(x => x > 0)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo");
            RuleFor(x => x.CessionarioCommittenteId)
                .Must(x => x > 0)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo");
            RuleFor(x => x.VettoreId)
                .Must(x => x > 0);
            RuleFor(x => x.MetadataId)
                .Must(x => x > 0)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo");

            RuleFor(x => x.TipoDocumento)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<TipoDocumento>())
                .WithName("TipoDocumento")
                .OverridePropertyName("tipoDocumento");
            RuleFor(x => x.Divisa)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<Divisa>())
                .WithName("Divisa")
                .OverridePropertyName("divisa");
            RuleFor(x => x.Data)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("data")
                .OverridePropertyName("data");
            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Matches(@"\d")
                .WithMessage("'{PropertyValue}': il campo non contiene caratteri numerici")
                .WithErrorCode("00425")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("numero")
                .OverridePropertyName("numero");

            RuleFor(x => x.BolloVirtuale)
                .Equal("SI")
                .WithMessage("'{PropertyName}': il campo accetta solo il valore 'SI'. Ricevuto '{PropertyValue}'")
                .WithName("BolloVirtuale")
                .OverridePropertyName("bolloVirtuale");
            RuleFor(x => x.ImportoBollo)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImportoBollo")
                .OverridePropertyName("importoBollo");


            RuleFor(x => x.ImportoTotaleDocumento)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImportoTotaleDocumento")
                .OverridePropertyName("importoTotaleDocumento");
            RuleFor(x => x.Arrotondamento)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("Arrotondamento")
                .OverridePropertyName("arrotondamento");
            RuleFor(x => x.Art73)
                .Equal("SI")
                .WithMessage("'{PropertyName}': il campo accetta solo il valore 'SI'. Ricevuto '{PropertyValue}'")
                .WithName("Art73")
                .OverridePropertyName("art73");

            RuleFor(x => x.NumeroFatturaPrincipale)
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Matches(@"\d")
                .WithMessage("'{PropertyValue}': il campo non contiene caratteri numerici")
                .WithErrorCode("00425")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("NumeroFatturaPrincipale")
                .OverridePropertyName("numeroFatturaPrincipale");

            RuleFor(x => x.MezzoTrasporto)
                .Length(1, 80)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("MezzoTrasporto")
                .OverridePropertyName("mezzoTrasporto");
            RuleFor(x => x.CausaleTrasporto)
                .Length(1, 100)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CausaleTrasporto")
                .OverridePropertyName("causaleTrasporto");
            RuleFor(x => x.Descrizione)
                .Length(1, 100)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Descrizione")
                .OverridePropertyName("descrizione");
            RuleFor(x => x.UnitaMisuraPeso)
                .Length(1, 10)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("UnitaMisuraPeso")
                .OverridePropertyName("unitaMisuraPeso");
            RuleFor(x => x.PesoLordo)
                .LessThanOrEqualTo(9999.99m)
                .WithMessage("'{PropertyName}': il campo non può avere un valore superiore a 9999.99. Ricevuto {PropertyValue}")
                .WithName("PesoLordo")
                .OverridePropertyName("pesoLordo");
            RuleFor(x => x.PesoNetto)
                .LessThanOrEqualTo(9999.99m)
                .WithMessage("'{PropertyName}': il campo non può avere un valore superiore a 9999.99. Ricevuto {PropertyValue}")
                .WithName("PesoNetto")
                .OverridePropertyName("pesoNetto");
            RuleFor(x => x.TipoResa)
                .SetValidator(new IsValidValidator<TipoResa>());
            RuleFor(x => x.LuogoResa)
                .SetValidator(new SediModelValidator());
            RuleForEach(x => x.Causale)
                .SetValidator(new CausaleModelValidator());
            RuleForEach(x => x.DatiBeniServizi)
                .SetValidator(new DatiBeniServiziModelValidator());
            RuleForEach(x => x.DatiCassaPrevidenziale)
                .SetValidator(new DatiCassaPrevidenzialeModelValidator());
            RuleForEach(x => x.DatiDDT)
                .SetValidator(new DatiDDTModelValidator());
            RuleForEach(x => x.Dati)
                .SetValidator(new DatiModelValidator());
            RuleForEach(x => x.DatiPagamento)
                .SetValidator(new DatiPagamentoModelValidator());
            RuleForEach(x => x.DatiRiepilogo)
                .SetValidator(new DatiRiepilogoModelValidator());
            RuleForEach(x => x.DatiRitenuta)
                .SetValidator(new DatiRitenutaModelValidator());
            RuleForEach(x => x.DatiSAL)
                .SetValidator(new DatiSALModelValidator());
            RuleForEach(x => x.DettaglioLinee)
                .SetValidator(new DettaglioLineeModelValidator());
            RuleFor(x => x.CedentePrestatore)
                .SetValidator(new CedentePrestatoreValidator());
            RuleFor(x => x.CessionarioCommittente)
                .SetValidator(new CessionarioCommittenteValidator());
            RuleFor(x => x.Vettore)
                .SetValidator(new VettoreModelValidator());
            RuleFor(x => x.Metadata)
                .SetValidator(new MetadataModelValidator());
        }
    }
}
