namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiBeniServiziDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public bool Emitted { get; set; }
        public string Descrizione { get; set; }
        public decimal Importo { get; set; }
        public decimal? Imposta { get; set; }
        public decimal? Aliquota { get; set; }
        public string Natura { get; set; }
        public string RiferimentoNormativo { get; set; }
    }
}