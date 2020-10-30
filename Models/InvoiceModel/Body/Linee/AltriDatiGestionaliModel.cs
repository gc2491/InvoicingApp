using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiBeniServizi;
using System;

namespace AspNET.Models.InvoiceModel.Body.Linee
{
    public class AltriDatiGestionaliModel
    {
        public int Id { get; set; }
        public int DettaglioLineeModelId { get; set; }
        public string TipoDato { get; set; }
        public string RiferimentoTesto { get; set; }
        public decimal? RiferimentoNumero { get; set; }
        public DateTime? RiferimentoData { get; set; }
        public DettaglioLineeModel DettaglioLinee { get; set; }

        public AltriDatiGestionaliModel() { }

        public AltriDatiGestionaliModel(AltriDatiGestionali adg)
        {
            if (adg != null)
            {
                this.TipoDato = adg.TipoDato;
                this.RiferimentoTesto = adg.RiferimentoTesto;
                this.RiferimentoNumero = adg.RiferimentoNumero;
                this.RiferimentoData = adg.RiferimentoData;
            }
        }
    }
}
