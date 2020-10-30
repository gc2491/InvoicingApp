using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body;
using System;
using System.Threading;
using System.Text.RegularExpressions;

namespace AspNET.Validators
{
    public class DettaglioLineeModelValidator : AbstractValidator<DettaglioLineeModel>
    {
        public DettaglioLineeModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.BodyModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("BodyModelId")
                .OverridePropertyName("bodyModelId");
            RuleFor(x => x.NumeroLinea)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x => x > 0)
                .WithMessage("'{PropertyName}': il campo deve essere maggiore di zero. Ricevuto {PropertyValue}")
                .WithName("NumeroLinea")
                .OverridePropertyName("numeroLinea");
            RuleFor(x => x.TipoCessionePrestazione)
                .SetValidator(new IsValidValidator<TipoCessionePrestazione>())
                .WithName("TipoCessionePrestazione")
                .OverridePropertyName("tipoCessionePrestazione");
            RuleFor(x => x.Descrizione)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(1, 1000)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Descrizione")
                .OverridePropertyName("descrizione");
            RuleFor(x => x.Quantita)
                .GreaterThanOrEqualTo(0)
                .WithMessage("'{PropertyName}': il campo non può essere negativo")
                .WithName("Quantita")
                .OverridePropertyName("quantita");
            RuleFor(x => x.UnitaMisura)
                .Length(1, 10)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("UnitaMisura")
                .OverridePropertyName("unitaMisura");
            RuleFor(x => x.PrezzoUnitario)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2,8}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 21 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("PrezzoUnitario")
                .OverridePropertyName("prezzoUnitario");
            RuleFor(x => x.PrezzoTotale)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2,2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .Must((dettaglioLinee, _) => PrezzoTotaleValidateAgainstError00423(dettaglioLinee))
                .WithMessage("'{PropertyName}': campo non calcolato secondo le specifiche tecniche")
                .WithErrorCode("00423")
                .WithName("PrezzoTotale")
                .OverridePropertyName("prezzoTotale");
            RuleFor(x => x.AliquotaIVA)
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,3}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("Lungezza massima 6 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("AliquotaIVA")
                .OverridePropertyName("aliquotaIVA");
            RuleFor(x => x.Ritenuta)
                .Equal("SI")
                .WithMessage("'{PropertyName}': il campo accetta solo il valore 'SI'. Ricevuto '{PropertyValue}'")
                .WithName("Ritenuta")
                .OverridePropertyName("ritenuta");
            RuleFor(x => x.Natura)
                .NotEmpty()
                .When(x => x.AliquotaIVA == 0)
                .WithMessage("'{PropertyName}': il campo non può essere nullo a fronte di Aliquota IVA pari a 0")
                .WithErrorCode("00400")
                .Empty()
                .When(x => x.AliquotaIVA > 0)
                .WithMessage("Natura presente a fronte di Aliquota IVA diversa da zero")
                .WithErrorCode("00401")
                .SetValidator(new IsValidValidator<Natura>())
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

            RuleForEach(x => x.AltriDatiGestionali)
                .SetValidator(new AltriDatiGestionaliModelValidator())
                .WithName("AltriDatiGestionali")
                .OverridePropertyName("altriDatiGestionali");
            RuleForEach(x => x.CodiceArticolo)
                .SetValidator(new CodiceArticoloModelValidator())
                .WithName("CodiceArticolo")
                .OverridePropertyName("codiceArticolo");
            RuleForEach(x => x.ScontoMaggiorazione)
                .SetValidator(new ScontoMaggiorazioneModelValidator())
                .WithName("ScontoMaggiorazione")
                .OverridePropertyName("scontoMaggiorazione");
        }

        private bool PrezzoTotaleValidateAgainstError00423(DettaglioLineeModel challenge)
        {
            var prezzo = Math.Round(challenge.PrezzoUnitario, 8, MidpointRounding.AwayFromZero);
            if (challenge.ScontoMaggiorazione != null)
            {
                foreach (var sconto in challenge.ScontoMaggiorazione)
                {

                    if (sconto.Importo == null && sconto.Percentuale == null) continue;

                    var importo =
                        (decimal)((sconto.Importo != null)
                        ? Math.Abs((decimal)sconto.Importo)
                        : (prezzo * sconto.Percentuale) / 100);

                    if (sconto.Tipo == "SC")
                        prezzo -= importo;
                    else
                        prezzo += importo;

                }
            }
            return Math.Abs(Math.Round(challenge.PrezzoTotale, 2, MidpointRounding.AwayFromZero)
                - prezzo * (challenge.Quantita ?? 1)) <= 0.01m;
        }
    }
}
