using FatturaElettronica.Semplificata.FatturaElettronicaBody.DatiBeniServizi;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiBeniServiziModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string Descrizione { get; set; }
        public decimal Importo { get; set; }
        public decimal? Imposta { get; set; }
        public decimal? Aliquota { get; set; }
        public string Natura { get; set; }
        public string RiferimentoNormativo { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiBeniServiziModel() { }

        public DatiBeniServiziModel(DatiBeniServizi beniServizi)
        {
            this.Descrizione = beniServizi.Descrizione;
            this.Importo = beniServizi.Importo;
            this.Imposta = beniServizi.DatiIVA.Imposta;
            this.Aliquota = beniServizi.DatiIVA.Aliquota;
            this.Natura = beniServizi.Natura;
            this.RiferimentoNormativo = beniServizi.RiferimentoNormativo;
        }
    }
}
