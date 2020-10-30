using AspNET.Models.Options.Invoices;
using FatturaElettronica.Ordinaria;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader;
using Semplificata = FatturaElettronica.Semplificata;
using System.Collections.Generic;
using AspNET.Models.ResultModel;

namespace AspNET.Models.InvoiceModel
{
    public class MetadataModel
    {
        public int Id { get; set; }
        public int TrasmittenteId { get; set; }
        public int? IntermediarioOEmittenteId { get; set; }
        public string ProgressivoInvio { get; set; }
        public string FormatoTrasmissione { get; set; }
        public string CodiceDestinatario { get; set; }
        public string SoggettoEmittente { get; set; }
        public string PECDestinatario { get; set; }
        public CliForModel Trasmittente { get; set; }
        public CliForModel IntermediarioOEmittente { get; set; }
        public List<BodyModel> Bodies { get; set; }

        public MetadataModel() { }

        public MetadataModel(ReadInvoiceResult result, OwnerOptions ownerOptions)
        {
            if (result.Ordinaria != null)
                InitializeInvoice(result.Ordinaria, ownerOptions);
            else
                InitializeInvoice(result.Semplificata, ownerOptions);
        }

        public void InitializeInvoice(FatturaOrdinaria fatturaElettronica, OwnerOptions ownerOptions)
        {
            FatturaElettronicaHeader header = fatturaElettronica.FatturaElettronicaHeader;

            this.ProgressivoInvio = header.DatiTrasmissione.ProgressivoInvio;
            this.FormatoTrasmissione = header.DatiTrasmissione.FormatoTrasmissione;
            this.CodiceDestinatario = header.DatiTrasmissione.CodiceDestinatario;
            this.SoggettoEmittente = header.SoggettoEmittente;
            this.PECDestinatario = header.DatiTrasmissione.PECDestinatario;

            this.Trasmittente = new CliForModel(header.DatiTrasmissione);

            if (header.TerzoIntermediarioOSoggettoEmittente != null)
            {
                this.IntermediarioOEmittente = new CliForModel(header.TerzoIntermediarioOSoggettoEmittente);
            }

            this.Bodies = new List<BodyModel>();
            foreach (var body in fatturaElettronica.FatturaElettronicaBody)
            {
                this.Bodies.Add(new BodyModel(body, header));
            }
        }

        public void InitializeInvoice(Semplificata.FatturaSemplificata semplificata, OwnerOptions ownerOptions)
        {
            Semplificata.FatturaElettronicaHeader.FatturaElettronicaHeader header = semplificata.FatturaElettronicaHeader;

            this.ProgressivoInvio = header.DatiTrasmissione.ProgressivoInvio;
            this.FormatoTrasmissione = header.DatiTrasmissione.FormatoTrasmissione;
            this.CodiceDestinatario = header.DatiTrasmissione.CodiceDestinatario;
            this.SoggettoEmittente = header.SoggettoEmittente;
            this.PECDestinatario = header.DatiTrasmissione.PECDestinatario;

            this.Trasmittente = new CliForModel(header.DatiTrasmissione);

            this.Bodies = new List<BodyModel>();
            foreach (var body in semplificata.FatturaElettronicaBody)
            {
                this.Bodies.Add(new BodyModel(body, header));
            }
        }
    }
}
