using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO.Body.Pagamenti
{
    public class ManyDettaglioPagamentoDTO
    {
        public ManyDettaglioPagamentoDTO()
        {
            this.Payments = new List<PaymentsDTO>();
        }

        public List<PaymentsDTO> Payments { get; set; }
        public int TotalPages { get; set; }
    }
}
