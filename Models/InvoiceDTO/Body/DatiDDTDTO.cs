using System;
using System.Collections.Generic;
using AspNET.Models.InvoiceDTO.Body.RiferimentoNumeroLinea;

namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiDDTDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public DateTime DataDDT { get; set; }
        public string NumeroDDT { get; set; }
        public List<RiferimentoNumeroLineaDTO> RiferimentoNumeroLinea { get; set; }
    }

}
