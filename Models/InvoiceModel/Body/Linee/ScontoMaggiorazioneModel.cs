using FatturaElettronica.Common;

namespace AspNET.Models.InvoiceModel.Body.Linee
{
    public class ScontoMaggiorazioneModel
    {
        public int Id { get; set; }
        public int DettaglioLineeId { get; set; }
        public string Tipo { get; set; }
        public decimal? Percentuale { get; set; }
        public decimal? Importo { get; set; }
        public DettaglioLineeModel DettaglioLinee { get; set; }

        public ScontoMaggiorazioneModel() { }

        public ScontoMaggiorazioneModel(ScontoMaggiorazione sm)
        {
            if (sm != null)
            {
                this.Tipo = sm.Tipo;
                this.Percentuale = sm.Percentuale;
                this.Importo = sm.Importo;
            }
        }
    }
}
