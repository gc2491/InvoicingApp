using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiRitenutaModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string TipoRitenuta { get; set; }
        public decimal? ImportoRitenuta { get; set; }
        public decimal? AliquotaRitenuta { get; set; }
        public string CausalePagamento { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiRitenutaModel() { }

        public DatiRitenutaModel(DatiRitenuta datiRitenuta)
        {
            this.TipoRitenuta = datiRitenuta.TipoRitenuta;
            this.ImportoRitenuta = datiRitenuta.ImportoRitenuta;
            this.AliquotaRitenuta = datiRitenuta.AliquotaRitenuta;
            this.CausalePagamento = datiRitenuta.CausalePagamento;
        }
    }
}
