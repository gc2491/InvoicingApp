namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiRitenutaDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string TipoRitenuta { get; set; }
        public decimal? ImportoRitenuta { get; set; }
        public decimal? AliquotaRitenuta { get; set; }
        public string CausalePagamento { get; set; }
    }
}