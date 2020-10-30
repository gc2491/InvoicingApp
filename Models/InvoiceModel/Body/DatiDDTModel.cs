using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;
using System;
using System.Collections.Generic;
using AspNET.Models.InvoiceModel.Body.RiferimentoNumeroLinea;

namespace AspNET.Models.InvoiceModel.Body
{
    public class DatiDDTModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public DateTime DataDDT { get; set; }
        public string NumeroDDT { get; set; }
        public List<RiferimentoNumeroLineaModel> RiferimentoNumeroLinea { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiDDTModel() { }

        public DatiDDTModel(DatiDDT ddt)
        {
            if (ddt != null)
            {
                this.DataDDT = ddt.DataDDT;
                this.NumeroDDT = ddt.NumeroDDT;

                this.RiferimentoNumeroLinea = new List<RiferimentoNumeroLineaModel>();
                int rifLength = ddt.RiferimentoNumeroLinea.Count;
                for (int i = 0; i < rifLength; i++)
                {
                    this.RiferimentoNumeroLinea
                        .Add(new RiferimentoNumeroLineaModel(ddt.RiferimentoNumeroLinea[i], this.GetType()));
                }
            }
        }
    }
}
