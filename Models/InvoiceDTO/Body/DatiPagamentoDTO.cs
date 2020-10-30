using AspNET.Models.InvoiceDTO.Body.Pagamenti;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiPagamentoDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string CondizioniPagamento { get; set; }
        public bool Active { get; set; }
        public List<DettaglioPagamentoDTO> DettaglioPagamento { get; set; }
    }

}
