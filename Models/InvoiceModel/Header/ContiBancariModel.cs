using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiPagamento;

namespace AspNET.Models.InvoiceModel.Header
{
    public class ContiBancariModel
    {
        public int Id { get; set; }
        public int CliforModelId { get; set; }
        public string IBAN { get; set; }
        public string ABI { get; set; }
        public string CAB { get; set; }
        public string BIC { get; set; }
        public string IstitutoFinanziario { get; set; }
        public bool StillActive { get; set; } = true;
        public CliForModel Clifor { get; set; }

        public ContiBancariModel() { }

        public ContiBancariModel(DettaglioPagamento dettaglioPagamenti)
        {
            if (dettaglioPagamenti != null)
            {
                this.IBAN = dettaglioPagamenti.IBAN;
                this.ABI = dettaglioPagamenti.ABI;
                this.CAB = dettaglioPagamenti.CAB;
                this.BIC = dettaglioPagamenti.BIC;
                this.IstitutoFinanziario = dettaglioPagamenti.IstitutoFinanziario;
            }
        }
    }
}
