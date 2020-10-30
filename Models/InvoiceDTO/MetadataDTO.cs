namespace AspNET.Models.InvoiceDTO
{
    public class MetadataDTO
    {
        public int Id { get; set; }
        public int? TrasmittenteId { get; set; }
        public int? IntermediarioOEmittenteId { get; set; }
        public string ProgressivoInvio { get; set; }
        public string FormatoTrasmissione { get; set; }
        public string CodiceDestinatario { get; set; }
        public string SoggettoEmittente { get; set; }
        public string PECDestinatario { get; set; }
        public CliForDTO Trasmittente { get; set; }
        public CliForDTO IntermediarioOEmittente { get; set; }
    }
}
