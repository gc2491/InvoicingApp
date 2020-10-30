using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.CedentePrestatore;
using FatturaElettronica.Ordinaria.FatturaElettronicaHeader.DatiTrasmissione;

namespace AspNET.Models.InvoiceModel.Header
{
    public class ContattiModel
    {
        public int Id { get; set; }
        public int CliforModelId { get; set; }
        public string Description { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool StillActive { get; set; } = true;
        public CliForModel Clifor { get; set; }

        public ContattiModel() { }

        public ContattiModel(Contatti contatti)
        {
            if (contatti != null)
            {
                this.Telefono = contatti.Telefono;
                this.Email = contatti.Email;
                this.Fax = contatti.Fax;
            }
        }

        public ContattiModel(ContattiTrasmittente contatti)
        {
            this.Telefono = contatti.Telefono;
            this.Email = contatti.Email;
        }
    }
}
