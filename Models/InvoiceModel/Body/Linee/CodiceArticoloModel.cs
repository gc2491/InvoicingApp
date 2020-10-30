using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiBeniServizi;

namespace AspNET.Models.InvoiceModel.Body.Linee
{
    public class CodiceArticoloModel
    {
        public int Id { get; set; }
        public int DettaglioLineeId { get; set; }
        public string CodiceTipo { get; set; }
        public string CodiceValore { get; set; }
        public DettaglioLineeModel DettaglioLinee { get; set; }

        public CodiceArticoloModel() { }

        public CodiceArticoloModel(CodiceArticolo ca)
        {
            if (ca != null)
            {
                this.CodiceTipo = ca.CodiceTipo;
                this.CodiceValore = ca.CodiceValore;
            }
        }
    }
}
