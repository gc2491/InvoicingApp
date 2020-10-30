using FatturaElettronica.Ordinaria.FatturaElettronicaBody;
using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiPagamento;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader;
using Semplificata = FatturaElettronica.Semplificata;
using AspNET.Models.InvoiceModel.Body;
using System;
using System.Collections.Generic;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using AspNET.Models.InvoiceModel.Header;

namespace AspNET.Models.InvoiceModel
{
    public class BodyModel
    {
        public int Id { get; set; }
        public int CedentePrestatoreId { get; set; }
        public int CessionarioCommittenteId { get; set; }
        public int? VettoreId { get; set; }
        public int MetadataId { get; set; }
        public int? LuogoResaId { get; set; }

        public string TipoDocumento { get; set; }
        public string Divisa { get; set; }
        public DateTime Data { get; set; }
        public string Numero { get; set; }

        public string BolloVirtuale { get; set; }
        public decimal? ImportoBollo { get; set; }

        public decimal? ImportoTotaleDocumento { get; set; }
        public decimal? Arrotondamento { get; set; }
        public string Art73 { get; set; }

        public string NumeroFatturaPrincipale { get; set; }
        public DateTime? DataFatturaPrincipale { get; set; }

        public string MezzoTrasporto { get; set; }
        public string CausaleTrasporto { get; set; }
        public int? NumeroColli { get; set; }
        public string Descrizione { get; set; }
        public string UnitaMisuraPeso { get; set; }
        public decimal? PesoLordo { get; set; }
        public decimal? PesoNetto { get; set; }
        public DateTime? DataOraRitiro { get; set; }
        public DateTime? DataInizioTrasporto { get; set; }
        public string TipoResa { get; set; }
        public DateTime? DataOraConsegna { get; set; }
        public SediModel LuogoResa { get; set; }
        public List<CausaleModel> Causale { get; set; }
        public List<DatiCassaPrevidenzialeModel> DatiCassaPrevidenziale { get; set; }
        public List<DatiDDTModel> DatiDDT { get; set; }
        public List<DatiModel> Dati { get; set; }
        public List<DatiPagamentoModel> DatiPagamento { get; set; }
        public List<DatiRiepilogoModel> DatiRiepilogo { get; set; }
        public List<DatiRitenutaModel> DatiRitenuta { get; set; }
        public List<DatiSALModel> DatiSAL { get; set; }
        public List<DettaglioLineeModel> DettaglioLinee { get; set; }
        public CliForModel CedentePrestatore { get; set; }
        public CliForModel CessionarioCommittente { get; set; }
        public CliForModel Vettore { get; set; }
        public MetadataModel Metadata { get; set; }

        // Fattura Semplificata
        public string NumeroFR { get; set; }
        public DateTime? DataFR { get; set; }
        public string ElementiRettificati { get; set; }
        public List<DatiBeniServiziModel> DatiBeniServizi { get; set; }
        public bool Simplified { get; set; }

        public BodyModel() { }

        public BodyModel(FatturaElettronicaBody body,
                         FatturaElettronicaHeader header)
        {
            if (body != null)
            {
                this.Simplified = false;

                this.TipoDocumento = body.DatiGenerali.DatiGeneraliDocumento.TipoDocumento;
                this.Divisa = body.DatiGenerali.DatiGeneraliDocumento.Divisa;
                this.Data = body.DatiGenerali.DatiGeneraliDocumento.Data;
                this.Numero = body.DatiGenerali.DatiGeneraliDocumento.Numero;
                this.BolloVirtuale = body.DatiGenerali.DatiGeneraliDocumento.DatiBollo.BolloVirtuale;
                this.ImportoBollo = body.DatiGenerali.DatiGeneraliDocumento.DatiBollo.ImportoBollo;
                this.ImportoTotaleDocumento = body.DatiGenerali.DatiGeneraliDocumento.ImportoTotaleDocumento;
                this.Arrotondamento = body.DatiGenerali.DatiGeneraliDocumento.Arrotondamento;
                this.Art73 = body.DatiGenerali.DatiGeneraliDocumento.Art73;
                this.NumeroFatturaPrincipale = body.DatiGenerali.FatturaPrincipale.NumeroFatturaPrincipale;
                this.DataFatturaPrincipale = body.DatiGenerali.FatturaPrincipale.DataFatturaPrincipale;

                this.MezzoTrasporto = body.DatiGenerali.DatiTrasporto.MezzoTrasporto;
                this.CausaleTrasporto = body.DatiGenerali.DatiTrasporto.CausaleTrasporto;
                this.NumeroColli = body.DatiGenerali.DatiTrasporto.NumeroColli;
                this.Descrizione = body.DatiGenerali.DatiTrasporto.Descrizione;
                this.UnitaMisuraPeso = body.DatiGenerali.DatiTrasporto.UnitaMisuraPeso;
                this.PesoLordo = body.DatiGenerali.DatiTrasporto.PesoLordo;
                this.PesoNetto = body.DatiGenerali.DatiTrasporto.PesoNetto;
                this.DataOraRitiro = body.DatiGenerali.DatiTrasporto.DataOraRitiro;
                this.DataInizioTrasporto = body.DatiGenerali.DatiTrasporto.DataInizioTrasporto;
                this.TipoResa = body.DatiGenerali.DatiTrasporto.TipoResa;
                this.DataOraConsegna = body.DatiGenerali.DatiTrasporto.DataOraConsegna;

                if (body.DatiGenerali.DatiTrasporto != null &&
                    body.DatiGenerali.DatiTrasporto.IndirizzoResa != null &&
                    body.DatiGenerali.DatiTrasporto.IndirizzoResa.CAP != null)
                {
                    this.LuogoResa = new SediModel(body.DatiGenerali.DatiTrasporto.IndirizzoResa);
                }

                int causaleLength = body.DatiGenerali.DatiGeneraliDocumento.Causale.Count;
                if (causaleLength > 0) this.Causale = new List<CausaleModel>();
                foreach (var c in body.DatiGenerali.DatiGeneraliDocumento.Causale)
                {
                    this.Causale.Add(new CausaleModel(c));
                }

                int ddtLength = body.DatiGenerali.DatiDDT.Count;
                if (ddtLength > 0) this.DatiDDT = new List<DatiDDTModel>();
                foreach (var ddt in body.DatiGenerali.DatiDDT)
                {
                    this.DatiDDT.Add(new DatiDDTModel(ddt));
                }

                int cassaLength = body.DatiGenerali.DatiGeneraliDocumento.DatiCassaPrevidenziale.Count;
                if (cassaLength > 0) this.DatiCassaPrevidenziale = new List<DatiCassaPrevidenzialeModel>();
                foreach (var cassa in body.DatiGenerali.DatiGeneraliDocumento.DatiCassaPrevidenziale)
                {
                    this.DatiCassaPrevidenziale.Add(new DatiCassaPrevidenzialeModel(cassa));
                }

                // ---DatiModel---
                int acquistoLength = body.DatiGenerali.DatiOrdineAcquisto.Count;
                int contrattoLength = body.DatiGenerali.DatiContratto.Count;
                int convenzioneLength = body.DatiGenerali.DatiConvenzione.Count;
                int ricezioneLength = body.DatiGenerali.DatiRicezione.Count;
                int fCollegateLength = body.DatiGenerali.DatiFattureCollegate.Count;
                if (acquistoLength > 0 ||
                    contrattoLength > 0 ||
                    convenzioneLength > 0 ||
                    ricezioneLength > 0 ||
                    fCollegateLength > 0
                    ) this.Dati = new List<DatiModel>();

                foreach (var acquisto in body.DatiGenerali.DatiOrdineAcquisto)
                {
                    this.Dati.Add(new DatiModel(acquisto));
                }

                foreach (var contratto in body.DatiGenerali.DatiContratto)
                {
                    this.Dati.Add(new DatiModel(contratto));
                }

                foreach (var convenzione in body.DatiGenerali.DatiConvenzione)
                {
                    this.Dati.Add(new DatiModel(convenzione));
                }

                foreach (var ricezione in body.DatiGenerali.DatiRicezione)
                {
                    this.Dati.Add(new DatiModel(ricezione));
                }

                foreach (var collegate in body.DatiGenerali.DatiFattureCollegate)
                {
                    this.Dati.Add(new DatiModel(collegate));
                }

                //---End DatiModel---

                this.DatiPagamento = new List<DatiPagamentoModel>();
                int pagamentoLength = body.DatiPagamento.Count;
                if (pagamentoLength > 0)
                {
                    foreach (var pagamento in body.DatiPagamento)
                    {
                        this.DatiPagamento.Add(new DatiPagamentoModel(pagamento));
                    }
                }
                else
                {
                    var defaultDettaglio = new DettaglioPagamentoModel
                    (
                        "MP05",
                        body.DatiGenerali.DatiGeneraliDocumento.ImportoTotaleDocumento ?? default(int),
                        body.DatiGenerali.DatiGeneraliDocumento.Data
                    );

                    var defaultPagamento = new DatiPagamentoModel
                    (
                        "TP02",
                        defaultDettaglio
                    );

                    this.DatiPagamento.Add(defaultPagamento);
                }

                this.DatiPagamento[0].Active = true;

                int riepilogoLength = body.DatiBeniServizi.DatiRiepilogo.Count;
                if (riepilogoLength > 0) this.DatiRiepilogo = new List<DatiRiepilogoModel>();
                foreach (var riepilogo in body.DatiBeniServizi.DatiRiepilogo)
                {
                    this.DatiRiepilogo.Add(new DatiRiepilogoModel(riepilogo));
                }

                int salLength = body.DatiGenerali.DatiSAL.Count;
                if (salLength > 0) this.DatiSAL = new List<DatiSALModel>();
                foreach (var sal in body.DatiGenerali.DatiSAL)
                {
                    this.DatiSAL.Add(new DatiSALModel(sal));
                }

                int lineeLength = body.DatiBeniServizi.DettaglioLinee.Count;
                if (lineeLength > 0) this.DettaglioLinee = new List<DettaglioLineeModel>();
                foreach (var linea in body.DatiBeniServizi.DettaglioLinee)
                {
                    this.DettaglioLinee.Add(new DettaglioLineeModel(linea));
                }

                int ritenutaLength = body.DatiGenerali.DatiGeneraliDocumento.DatiRitenuta.Count;
                if (ritenutaLength > 0)
                {
                    this.DatiRitenuta = new List<DatiRitenutaModel>();

                    foreach (var r in body.DatiGenerali.DatiGeneraliDocumento.DatiRitenuta)
                    {
                        this.DatiRitenuta.Add(new DatiRitenutaModel(r));
                    }

                }

                DettaglioPagamento dettaglioPagamento = new DettaglioPagamento();
                if (body.DatiPagamento.Count > 0 && body.DatiPagamento[0].DettaglioPagamento.Count > 0)
                {
                    dettaglioPagamento = body.DatiPagamento[0].DettaglioPagamento[0];
                }

                this.CedentePrestatore = new CliForModel(header.CedentePrestatore, header.Rappresentante, dettaglioPagamento);

                this.CessionarioCommittente = new CliForModel(header.CessionarioCommittente);

                if (body.DatiGenerali.DatiTrasporto.DatiAnagraficiVettore != null)
                {
                    this.Vettore = new CliForModel(body.DatiGenerali.DatiTrasporto.DatiAnagraficiVettore);
                }
            }
        }

        public BodyModel(Semplificata.FatturaElettronicaBody.FatturaElettronicaBody body,
                         Semplificata.FatturaElettronicaHeader.FatturaElettronicaHeader header)
        {
            if (body != null)
            {
                this.Simplified = true;

                this.TipoDocumento = body.DatiGenerali.DatiGeneraliDocumento.TipoDocumento;
                this.Divisa = body.DatiGenerali.DatiGeneraliDocumento.Divisa;
                this.Data = body.DatiGenerali.DatiGeneraliDocumento.Data;
                this.Numero = body.DatiGenerali.DatiGeneraliDocumento.Numero;
                this.BolloVirtuale = body.DatiGenerali.DatiGeneraliDocumento.BolloVirtuale;

                int beniServiziLength = body.DatiBeniServizi.Count;
                if (beniServiziLength > 0) this.DatiBeniServizi = new List<DatiBeniServiziModel>();
                foreach (var linea in body.DatiBeniServizi)
                {
                    this.DatiBeniServizi.Add(new DatiBeniServiziModel(linea));
                }

                decimal importoTotaleDocumento = 0;

                foreach (var beni in body.DatiBeniServizi)
                {
                    importoTotaleDocumento += beni.Importo;
                    importoTotaleDocumento += beni.DatiIVA.Imposta ?? 0;
                }

                var defaultDettaglio = new DettaglioPagamentoModel
                (
                    "MP05",
                    importoTotaleDocumento,
                    body.DatiGenerali.DatiGeneraliDocumento.Data
                );

                var defaultPagamento = new DatiPagamentoModel
                (
                    "TP02",
                    defaultDettaglio
                );

                this.DatiPagamento = new List<DatiPagamentoModel>();
                this.DatiPagamento.Add(defaultPagamento);

                this.DatiPagamento[0].Active = true;

                this.CedentePrestatore = new CliForModel(header.CedentePrestatore);

                this.CessionarioCommittente = new CliForModel(header.CessionarioCommittente);
            }
        }
    }
}
