using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiPagamento;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiPagamentoModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string CondizioniPagamento { get; set; }
        public bool Active { get; set; }
        public List<DettaglioPagamentoModel> DettaglioPagamento { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiPagamentoModel() { }

        public DatiPagamentoModel(DatiPagamento dp)
        {
            if (dp != null)
            {
                this.CondizioniPagamento = dp.CondizioniPagamento;

                this.DettaglioPagamento = new List<DettaglioPagamentoModel>();
                int dettaglioLength = dp.DettaglioPagamento.Count;
                for (int i = 0; i < dettaglioLength; i++)
                {
                    this.DettaglioPagamento.Add(new DettaglioPagamentoModel(dp.DettaglioPagamento[i]));
                }
            }
        }

        public DatiPagamentoModel(string defaultCondizioniPagamento, DettaglioPagamentoModel defaultDettaglio)
        {
            this.CondizioniPagamento = defaultCondizioniPagamento;
            this.DettaglioPagamento = new List<DettaglioPagamentoModel>();
            this.DettaglioPagamento.Add(defaultDettaglio);
        }
    }
}
