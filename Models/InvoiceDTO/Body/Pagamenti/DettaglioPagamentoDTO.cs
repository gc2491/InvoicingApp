using System;

namespace AspNET.Models.InvoiceDTO.Body.Pagamenti
{
    public class DettaglioPagamentoDTO
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
    }

}
