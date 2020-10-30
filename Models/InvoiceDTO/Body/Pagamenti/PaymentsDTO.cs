using AspNET.Models.InvoiceDTO.Header;
using AspNET.Models.InvoiceModel.Body.Pagamenti;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO.Body.Pagamenti
{
    public class PaymentsDTO
    {
        public PaymentsDTO()
        {
            this.DettaglioPagamento = new DettaglioPagamentoDTO();
        }

        public DettaglioPagamentoDTO DettaglioPagamento { get; set; }
        public int CliforId { get; set; }
        public string Clifor { get; set; }
        public List<ContiBancariDTO> ContiBancari { get; set; }
        public string Numero { get; set; }
        public DateTime Data { get; set; }
    }
}
