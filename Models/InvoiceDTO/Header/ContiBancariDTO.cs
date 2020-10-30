namespace AspNET.Models.InvoiceDTO.Header
{
    public class ContiBancariDTO
    {
        public int Id { get; set; }
        public int CliforModelId { get; set; }
        public string IBAN { get; set; }
        public string ABI { get; set; }
        public string CAB { get; set; }
        public string BIC { get; set; }
        public string IstitutoFinanziario { get; set; }
    }
}
