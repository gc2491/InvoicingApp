namespace AspNET.Models.InvoiceDTO.Body
{
    public class DatiCassaPrevidenzialeDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string TipoCassa { get; set; }
        public decimal AlCassa { get; set; }
        public decimal ImportoContributoCassa { get; set; }
        public decimal ImponibileCassa { get; set; }
        public decimal AliquotaIVA { get; set; }
        public string Ritenuta { get; set; }
        public string Natura { get; set; }
        public string RiferimentoAmministrazione { get; set; }
    }

}
