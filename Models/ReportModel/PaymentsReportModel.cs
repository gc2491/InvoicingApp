using System;

namespace AspNET.Models.ReportModel
{
    public class PaymentsReportModel
    {
        public string NumeroFattura { get; set; }
        public DateTime DataFattura { get; set; }
        public string ModalitaPagamento {get;set;}
        public string IstitutoFinanziario { get; set; }
        public string IBAN { get; set; }
        public string TRNCode { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DataScadenzaPagamento  { get; set; }
        public decimal ImportoPagamento {get;set;}
    }
}
