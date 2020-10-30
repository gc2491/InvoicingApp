namespace AspNET.Models.InvoiceModel.Body
{
    public class CausaleModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string Causale { get; set; }
        public BodyModel BodyModel { get; set; }

        public CausaleModel() { }

        public CausaleModel(string causale)
        {
            this.Causale = causale;
        }
    }
}
