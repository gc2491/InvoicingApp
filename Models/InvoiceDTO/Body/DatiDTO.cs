using AspNET.Models.InvoiceDTO.Body.RiferimentoNumeroLinea;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string DataType { get; set; }
        public string IdDocumento { get; set; }
        public DateTime? Data { get; set; }
        public string NumItem { get; set; }
        public string CodiceCommessaConvenzione { get; set; }
        public string CodiceCUP { get; set; }
        public string CodiceCIG { get; set; }
        public List<RiferimentoNumeroLineaDTO> RiferimentoNumeroLinea { get; set; }
    }
}
