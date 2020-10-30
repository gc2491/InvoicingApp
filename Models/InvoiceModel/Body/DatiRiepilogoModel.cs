using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiBeniServizi;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiRiepilogoModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public decimal AliquotaIVA { get; set; }
        public string Natura { get; set; }
        public decimal? SpeseAccessorie { get; set; }
        public decimal? Arrotondamento { get; set; }
        public decimal ImponibileImporto { get; set; }
        public decimal Imposta { get; set; }
        public string EsigibilitaIVA { get; set; }
        public string RiferimentoNormativo { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiRiepilogoModel() { }

        public DatiRiepilogoModel(DatiRiepilogo dr)
        {
            if (dr != null)
            {
                this.AliquotaIVA = dr.AliquotaIVA;
                this.Natura = dr.Natura;
                this.SpeseAccessorie = dr.SpeseAccessorie;
                this.Arrotondamento = dr.Arrotondamento;
                this.ImponibileImporto = dr.ImponibileImporto;
                this.Imposta = dr.Imposta;
                this.EsigibilitaIVA = dr.EsigibilitaIVA;
                this.RiferimentoNormativo = dr.RiferimentoNormativo;
            }
        }
    }
}
