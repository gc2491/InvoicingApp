namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiRiepilogoDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public decimal AliquotaIVA { get; set; }
        public string Natura { get; set; }
        public decimal? SpeseAccessorie { get; set; }
        public decimal? Arrotondamento { get; set; }
        public decimal ImponibileImporto { get; set; }
        public decimal Imposta { get; set; }
        public string EsigibilitaIVA { get; set; }
        public string RiferimentoNormativo { get; set; }
    }

}
