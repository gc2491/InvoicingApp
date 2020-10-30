using System;

namespace AspNET.Models.InvoiceDTO.Body.Linee
{
    public class AltriDatiGestionaliDTO
    {
        public int Id { get; set; }
        public int DettaglioLineeDettaglioLineeModelId { get; set; }
        public string TipoDato { get; set; }
        public string RiferimentoTesto { get; set; }
        public decimal? RiferimentoNumero { get; set; }
        public DateTime? RiferimentoData { get; set; }
    }

}
