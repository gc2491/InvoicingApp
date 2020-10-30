using FatturaElettronica.Common;
using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.CedentePrestatore;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.CessionarioCommittente;
using CedentePrestatoreSimplified = FatturaElettronica.Semplificata.FatturaElettronicaHeader.CedentePrestatore;
using CessionarioCommittenteSimplified = FatturaElettronica.Semplificata.FatturaElettronicaHeader.CessionarioCommittente;

namespace AspNET.Models.InvoiceModel.Header
{
    public class SediModel
    {
        public int Id { get; set; }
        public int? CliforModelId { get; set; }
        public int? StabileOrganizzazioneId { get; set; }
        public string Description { get; set; }
        public string Indirizzo { get; set; }
        public string NumeroCivico { get; set; }
        public string CAP { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public string Nazione { get; set; }
        public bool StillActive { get; set; } = true;
        public CliForModel Clifor { get; set; }
        public CliForModel StabileOrganizzazione { get; set; }
        public BodyModel Body { get; set; }

        public SediModel() { }

        public SediModel(SedeCedentePrestatore sede)
        {
            if (sede != null)
            {
                this.Indirizzo = sede.Indirizzo;
                this.NumeroCivico = sede.NumeroCivico;
                this.CAP = sede.CAP;
                this.Comune = sede.Comune;
                this.Provincia = sede.Provincia;
                this.Nazione = sede.Nazione;
            }
        }

        public SediModel(SedeCessionarioCommittente sede)
        {
            if (sede != null)
            {
                this.Indirizzo = sede.Indirizzo;
                this.NumeroCivico = sede.NumeroCivico;
                this.CAP = sede.CAP;
                this.Comune = sede.Comune;
                this.Provincia = sede.Provincia;
                this.Nazione = sede.Nazione;
            }
        }

        public SediModel(CedentePrestatoreSimplified.SedeCedentePrestatore sede)
        {
            if (sede != null)
            {
                this.Indirizzo = sede.Indirizzo;
                this.NumeroCivico = sede.NumeroCivico;
                this.CAP = sede.CAP;
                this.Comune = sede.Comune;
                this.Provincia = sede.Provincia;
                this.Nazione = sede.Nazione;
            }
        }

        public SediModel(CessionarioCommittenteSimplified.SedeCessionarioCommittente sede)
        {
            if (sede != null)
            {
                this.Indirizzo = sede.Indirizzo;
                this.NumeroCivico = sede.NumeroCivico;
                this.CAP = sede.CAP;
                this.Comune = sede.Comune;
                this.Provincia = sede.Provincia;
                this.Nazione = sede.Nazione;
            }
        }

        public SediModel(StabileOrganizzazione sede)
        {
            if (sede != null)
            {
                this.Indirizzo = sede.Indirizzo;
                this.NumeroCivico = sede.NumeroCivico;
                this.CAP = sede.CAP;
                this.Comune = sede.Comune;
                this.Provincia = sede.Provincia;
                this.Nazione = sede.Nazione;
            }
        }

        public SediModel(IndirizzoResa sede)
        {
            if (sede != null)
            {
                this.Indirizzo = sede.Indirizzo;
                this.NumeroCivico = sede.NumeroCivico;
                this.CAP = sede.CAP;
                this.Comune = sede.Comune;
                this.Provincia = sede.Provincia;
                this.Nazione = sede.Nazione;
            }
        }
    }
}
