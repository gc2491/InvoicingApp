using AspNET.Models.InvoiceModel.Body.RiferimentoNumeroLinea;
using FatturaElettronica.Ordinaria.FatturaElettronicaBody.DatiGenerali;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceModel.Body
{
    // The following data are stored in this model
    //      DatiOrdineAcquisto
    //      DatiContratto
    //      DatiConvenzione
    //      DatiRicezione
    //      DatiFattureCollegate
    public class DatiModel
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public string DataType { get; set; }
        public string IdDocumento { get; set; }
        public DateTime? Data { get; set; }
        public string NumItem { get; set; }
        public string CodiceCommessaConvenzione { get; set; }
        public string CodiceCUP { get; set; }
        public string CodiceCIG { get; set; }
        public List<RiferimentoNumeroLineaModel> RiferimentoNumeroLinea { get; set; }
        public BodyModel BodyModel { get; set; }

        public DatiModel() { }

        public DatiModel(DatiOrdineAcquisto dati)
        {
            if (dati != null)
            {
                this.DataType = "DatiOrdineAcquisto";
                this.IdDocumento = dati.IdDocumento;
                this.Data = dati.Data;
                this.NumItem = dati.NumItem;
                this.CodiceCommessaConvenzione = dati.CodiceCommessaConvenzione;
                this.CodiceCUP = dati.CodiceCUP;
                this.CodiceCIG = dati.CodiceCIG;

                this.RiferimentoNumeroLinea = new List<RiferimentoNumeroLineaModel>();
                int length = dati.RiferimentoNumeroLinea.Count;
                for (int i = 0; i < length; i++)
                {
                    RiferimentoNumeroLineaModel temp = new RiferimentoNumeroLineaModel(dati.RiferimentoNumeroLinea[i], this.GetType());
                    this.RiferimentoNumeroLinea.Add(temp);
                }
            }
        }

        public DatiModel(DatiContratto dati)
        {
            if (dati != null)
            {
                this.DataType = "DatiContratto";
                this.IdDocumento = dati.IdDocumento;
                this.Data = dati.Data;
                this.NumItem = dati.NumItem;
                this.CodiceCommessaConvenzione = dati.CodiceCommessaConvenzione;
                this.CodiceCUP = dati.CodiceCUP;
                this.CodiceCIG = dati.CodiceCIG;

                this.RiferimentoNumeroLinea = new List<RiferimentoNumeroLineaModel>();
                int length = dati.RiferimentoNumeroLinea.Count;
                for (int i = 0; i < length; i++)
                {
                    RiferimentoNumeroLineaModel temp = new RiferimentoNumeroLineaModel(dati.RiferimentoNumeroLinea[i], this.GetType());
                    this.RiferimentoNumeroLinea.Add(temp);
                }
            }
        }

        public DatiModel(DatiConvenzione dati)
        {
            if (dati != null)
            {
                this.DataType = "DatiConvenzione";
                this.IdDocumento = dati.IdDocumento;
                this.Data = dati.Data;
                this.NumItem = dati.NumItem;
                this.CodiceCommessaConvenzione = dati.CodiceCommessaConvenzione;
                this.CodiceCUP = dati.CodiceCUP;
                this.CodiceCIG = dati.CodiceCIG;

                this.RiferimentoNumeroLinea = new List<RiferimentoNumeroLineaModel>();
                int length = dati.RiferimentoNumeroLinea.Count;
                for (int i = 0; i < length; i++)
                {
                    RiferimentoNumeroLineaModel temp = new RiferimentoNumeroLineaModel(dati.RiferimentoNumeroLinea[i], this.GetType());
                    this.RiferimentoNumeroLinea.Add(temp);
                }
            }
        }

        public DatiModel(DatiRicezione dati)
        {
            if (dati != null)
            {
                this.DataType = "DatiRicezione";
                this.IdDocumento = dati.IdDocumento;
                this.Data = dati.Data;
                this.NumItem = dati.NumItem;
                this.CodiceCommessaConvenzione = dati.CodiceCommessaConvenzione;
                this.CodiceCUP = dati.CodiceCUP;
                this.CodiceCIG = dati.CodiceCIG;

                this.RiferimentoNumeroLinea = new List<RiferimentoNumeroLineaModel>();
                int length = dati.RiferimentoNumeroLinea.Count;
                for (int i = 0; i < length; i++)
                {
                    RiferimentoNumeroLineaModel temp = new RiferimentoNumeroLineaModel(dati.RiferimentoNumeroLinea[i], this.GetType());
                    this.RiferimentoNumeroLinea.Add(temp);
                }
            }
        }

        public DatiModel(DatiFattureCollegate dati)
        {
            if (dati != null)
            {
                this.DataType = "DatiFattureCollegate";
                this.IdDocumento = dati.IdDocumento;
                this.Data = dati.Data;
                this.NumItem = dati.NumItem;
                this.CodiceCommessaConvenzione = dati.CodiceCommessaConvenzione;
                this.CodiceCUP = dati.CodiceCUP;
                this.CodiceCIG = dati.CodiceCIG;

                this.RiferimentoNumeroLinea = new List<RiferimentoNumeroLineaModel>();
                int length = dati.RiferimentoNumeroLinea.Count;
                for (int i = 0; i < length; i++)
                {
                    RiferimentoNumeroLineaModel temp = new RiferimentoNumeroLineaModel(dati.RiferimentoNumeroLinea[i], this.GetType());
                    this.RiferimentoNumeroLinea.Add(temp);
                }
            }
        }
    }
}
