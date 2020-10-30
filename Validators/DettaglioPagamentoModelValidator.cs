using FatturaElettronica.Tabelle;
using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using System.Threading;
using System.Text.RegularExpressions;

namespace AspNET.Validators
{
    public class DettaglioPagamentoModelValidator : AbstractValidator<DettaglioPagamentoModel>
    {
        public DettaglioPagamentoModelValidator()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            RuleFor(x => x.DatiPagamentoModelId)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .WithName("DatiPagamentoModelId")
                .OverridePropertyName("datiPagamentoModelId");
            RuleFor(x => x.ModalitaPagamento)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .SetValidator(new IsValidValidator<ModalitaPagamento>())
                .WithName("ModalitaPagamento")
                .OverridePropertyName("modalitaPagamento");
            RuleFor(x => x.PaymentDate)
                .NotEmpty()
                .When(x => x.ContoBancarioId != null)
                .WithMessage("'{PropertyName}': il campo non può essere nullo quando il conto bancario non è nullo")
                .WithName("DataPagamento")
                .OverridePropertyName("paymentDate");
            RuleFor(x => x.ContoBancarioId)
                .NotEmpty()
                .When(x => x.TRNCode != null)
                .WithMessage("'{PropertyName}': il campo deve essere definito se il codice TRN non è nullo")
                .WithName("ContoBancario")
                .OverridePropertyName("contoBancarioId");
            RuleFor(x => x.TRNCode)
                .Length(30, 30)
                .WithMessage("'{PropertyName}': il campo deve essere composto da {MinLength} caratteri. Ricevuti: {TotalLength}")
                .Matches("^[A-Za-z0-9]+$")
                .WithMessage("'{PropertyName}': il campo può contenere solo caratteri alfanumerici")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("TRNCode")
                .OverridePropertyName("trnCode");
            RuleFor(x => x.ImportoPagamento)
                .NotEmpty()
                .Must(x =>
                {
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (massimo due, separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ImportoPagamento")
                .OverridePropertyName("importoPagamento");
            RuleFor(x => x.CodUfficioPostale)
                .Length(1, 20)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodUfficioPostale")
                .OverridePropertyName("codUfficioPostale");
            RuleFor(x => x.CognomeQuietanzante)
                .Length(1, 60)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CognomeQuietanzante")
                .OverridePropertyName("cognomeQuietanzante");
            RuleFor(x => x.NomeQuietanzante)
                .Length(1, 60)
                .WithMessage("'{PropertyValue}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti {TotalLength}")
                .BasicLatinValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non validi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("NomeQuietanzante")
                .OverridePropertyName("nomeQuietanzante");
            RuleFor(x => x.CFQuietanzante)
                .Length(1, 16)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non ammessi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CFQuietanzante")
                .OverridePropertyName("cfQuietanzante");
            RuleFor(x => x.TitoloQuietanzante)
                .Length(2, 10)
                .WithMessage("'{PropertyName}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non ammessi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("TitoloQuietanzante")
                .OverridePropertyName("titoloQuietanzante");
            RuleFor(x => x.ScontoPagamentoAnticipato)
                .Must(x =>
                {
                    if (x == null) return true;
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .When(x => x != null)
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("ScontoPagamentoAnticipato")
                .OverridePropertyName("scontoPagamentoAnticipato");
            RuleFor(x => x.PenalitaPagamentiRitardati)
                .Must(x =>
                {
                    if (x == null) return true;
                    Match result = Regex.Match(x.ToString(), @"^\d{1,12}\.\d{2}$");

                    return result.Success;
                })
                .WithMessage("'{PropertyName}': Lungezza massima 15 caratteri, i decimali (separati da un punto) vanno specificati anche se pari a 0 (es: 0.00). Valore ricevuto: {PropertyValue}")
                .WithName("PenalitaPagamentiRitardati")
                .OverridePropertyName("penalitaPagamentiRitardati");
            RuleFor(x => x.CodicePagamento)
                .Length(1, 60)
                .WithMessage("'{PropertyName}': il campo può contenere tra {MinLength} e {MaxLength} caratteri. Ricevuti: {TotalLength}")
                .Latin1SupplementValidator()
                .WithMessage("'{PropertyName}': il campo contiene caratteri non ammessi")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("CodicePagamento")
                .OverridePropertyName("codicePagamento");
        }
    }
}
