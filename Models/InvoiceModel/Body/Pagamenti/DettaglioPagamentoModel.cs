using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiPagamento;
using System;

namespace AspNET.Models.InvoiceModel.Body.Pagamenti
{
    public class DettaglioPagamentoModel
    {
        public int Id { get; set; }
        public int DatiPagamentoModelId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string TRNCode { get; set; }
        public int? ContoBancarioId { get; set; }
        public string ModalitaPagamento { get; set; }
        public DateTime? DataRiferimentoTerminiPagamento { get; set; }
        public int? GiorniTerminiPagamento { get; set; }
        public DateTime? DataScadenzaPagamento { get; set; }
        public decimal ImportoPagamento { get; set; }
        public string CodUfficioPostale { get; set; }
        public string CognomeQuietanzante { get; set; }
        public string NomeQuietanzante { get; set; }
        public string CFQuietanzante { get; set; }
        public string TitoloQuietanzante { get; set; }
        public decimal? ScontoPagamentoAnticipato { get; set; }
        public DateTime? DataLimitePagamentoAnticipato { get; set; }
        public decimal? PenalitaPagamentiRitardati { get; set; }
        public DateTime? DataDecorrenzaPenale { get; set; }
        public string CodicePagamento { get; set; }
        public DatiPagamentoModel DatiPagamento { get; set; }

        public DettaglioPagamentoModel() { }

        public DettaglioPagamentoModel(DettaglioPagamento dp)
        {
            if (dp != null)
            {
                this.ModalitaPagamento = dp.ModalitaPagamento;
                this.DataRiferimentoTerminiPagamento = dp.DataRiferimentoTerminiPagamento;
                this.GiorniTerminiPagamento = dp.GiorniTerminiPagamento;
                this.DataScadenzaPagamento = dp.DataScadenzaPagamento;
                this.ImportoPagamento = dp.ImportoPagamento;
                this.CodUfficioPostale = dp.CodUfficioPostale;
                this.CognomeQuietanzante = dp.CognomeQuietanzante;
                this.NomeQuietanzante = dp.NomeQuietanzante;
                this.CFQuietanzante = dp.CFQuietanzante;
                this.TitoloQuietanzante = dp.TitoloQuietanzante;
                this.ScontoPagamentoAnticipato = dp.ScontoPagamentoAnticipato;
                this.DataLimitePagamentoAnticipato = dp.DataLimitePagamentoAnticipato;
                this.PenalitaPagamentiRitardati = dp.PenalitaPagamentiRitardati;
                this.DataDecorrenzaPenale = dp.DataDecorrenzaPenale;
                this.CodicePagamento = dp.CodicePagamento;
            }
        }

        public DettaglioPagamentoModel(
            string modalitaPagamento,
            decimal importoPagamento,
            DateTime data)
        {
            this.ModalitaPagamento = modalitaPagamento;
            this.ImportoPagamento = importoPagamento;
            this.DataScadenzaPagamento = data;
        }
    }
}
