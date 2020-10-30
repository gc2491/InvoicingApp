using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiSALModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public int RiferimentoFase { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiSALModel() { }

        public DatiSALModel(DatiSAL sal)
        {
            if (sal != null)
                this.RiferimentoFase = sal.RiferimentoFase;
        }
    }
}
