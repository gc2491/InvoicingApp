namespace AspNET.Models.InvoiceDTO.Body.Linee
{
    public class ScontoMaggiorazioneDTO
    {
        public int Id { get; set; }
        public int DettaglioLineeId { get; set; }
        public string Tipo { get; set; }
        public decimal? Percentuale { get; set; }
        public decimal? Importo { get; set; }
    }

}
