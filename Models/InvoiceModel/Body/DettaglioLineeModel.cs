using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiBeniServizi;
using AspNET.Models.InvoiceModel.Body.Linee;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DettaglioLineeModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public int NumeroLinea { get; set; }
        public string TipoCessionePrestazione { get; set; }
        public string Descrizione { get; set; }
        public decimal? Quantita { get; set; }
        public string UnitaMisura { get; set; }
        public DateTime? DataInizioPeriodo { get; set; }
        public DateTime? DataFinePeriodo { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public decimal PrezzoTotale { get; set; }
        public decimal AliquotaIVA { get; set; }
        public string Ritenuta { get; set; }
        public string Natura { get; set; }
        public string RiferimentoAmministrazione { get; set; }
        public BodyModel BodyModel { get; set; }
        public List<CodiceArticoloModel> CodiceArticolo { get; set; }
        public List<ScontoMaggiorazioneModel> ScontoMaggiorazione { get; set; }
        public List<AltriDatiGestionaliModel> AltriDatiGestionali { get; set; }

        public DettaglioLineeModel() { }

        public DettaglioLineeModel(DettaglioLinee dl)
        {
            if (dl != null)
            {
                this.NumeroLinea = dl.NumeroLinea;
                this.TipoCessionePrestazione = dl.TipoCessionePrestazione;
                this.Descrizione = dl.Descrizione;
                this.Quantita = dl.Quantita;
                this.UnitaMisura = dl.UnitaMisura;
                this.DataInizioPeriodo = dl.DataInizioPeriodo;
                this.DataFinePeriodo = dl.DataFinePeriodo;
                this.PrezzoUnitario = dl.PrezzoUnitario;
                this.PrezzoTotale = dl.PrezzoTotale;
                this.AliquotaIVA = dl.AliquotaIVA;
                this.Ritenuta = dl.Ritenuta;
                this.Natura = dl.Natura;
                this.RiferimentoAmministrazione = dl.RiferimentoAmministrazione;

                this.AltriDatiGestionali = new List<AltriDatiGestionaliModel>();
                int adgLength = dl.AltriDatiGestionali.Count;
                for (int i = 0; i < adgLength; i++)
                {
                    this.AltriDatiGestionali.Add(new AltriDatiGestionaliModel(dl.AltriDatiGestionali[i]));
                }

                this.CodiceArticolo = new List<CodiceArticoloModel>();
                int artLength = dl.CodiceArticolo.Count;
                for (int i = 0; i < artLength; i++)
                {
                    this.CodiceArticolo.Add(new CodiceArticoloModel(dl.CodiceArticolo[i]));
                }

                this.ScontoMaggiorazione = new List<ScontoMaggiorazioneModel>();
                int smLength = dl.ScontoMaggiorazione.Count;
                for (int i = 0; i < smLength; i++)
                {
                    this.ScontoMaggiorazione.Add(new ScontoMaggiorazioneModel(dl.ScontoMaggiorazione[i]));
                }
            }
        }
    }
}
