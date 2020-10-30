using System;

namespace AspNET.Models.InvoiceModel.Body.RiferimentoNumeroLinea
{
    public class RiferimentoNumeroLineaModel
    {
        public int Id { get; set; }
        public int? DatiModelId { get; set; }
        public int? DatiDDTModelId { get; set; }
        public string ModelReference { get; set; }
        public int RiferimentoNumeroLinea { get; set; }
        public DatiModel DatiModel { get; set; }
        public DatiDDTModel DatiDDTModel { get; set; }

        public RiferimentoNumeroLineaModel() { }

        public RiferimentoNumeroLineaModel(int riferimento, Type modelType)
        {
            this.ModelReference = modelType == typeof(DatiModel) ?
                "DatiModel" : "DatiDDT";

            this.RiferimentoNumeroLinea = riferimento;
        }
    }
}
