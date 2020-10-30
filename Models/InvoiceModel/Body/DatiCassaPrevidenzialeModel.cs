using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiCassaPrevidenzialeModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string TipoCassa { get; set; }
        public decimal AlCassa { get; set; }
        public decimal ImportoContributoCassa { get; set; }
        public decimal ImponibileCassa { get; set; }
        public decimal AliquotaIVA { get; set; }
        public string Ritenuta { get; set; }
        public string Natura { get; set; }
        public string RiferimentoAmministrazione { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiCassaPrevidenzialeModel() { }

        public DatiCassaPrevidenzialeModel(DatiCassaPrevidenziale dcp)
        {
            if (dcp != null)
            {
                this.TipoCassa = dcp.TipoCassa;
                this.AlCassa = dcp.AlCassa;
                this.ImportoContributoCassa = dcp.ImportoContributoCassa;
                this.ImponibileCassa = dcp.ImponibileCassa;
                this.AliquotaIVA = dcp.AliquotaIVA;
                this.Ritenuta = dcp.Ritenuta;
                this.Natura = dcp.Natura;
                this.RiferimentoAmministrazione = dcp.RiferimentoAmministrazione;
            }
        }
    }
}
