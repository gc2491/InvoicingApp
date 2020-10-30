namespace AspNET.Models.InvoiceDTO.Header
{
    public class SediDTO
    {
        public int Id { get; set; }
        public int CliforModelId { get; set; }
        public string Description { get; set; }
        public string Indirizzo { get; set; }
        public string NumeroCivico { get; set; }
        public string CAP { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }
        public string Nazione { get; set; }
    }
}