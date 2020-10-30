using FatturaElettronica.Tabelle;

namespace AspNET.Customizations.FatturaElettronicaNuGet.Tabelle
{
    public class DataType : Tabella
    {
        public override Tabella[] List
        {
            get
            {
                return new Tabella[] {
                    new DataType { Codice = "DatiOrdineAcquisto", Nome = "DatiOrdineAcquisto" },
                    new DataType { Codice = "DatiContratto", Nome = "DatiContratto" },
                    new DataType { Codice = "DatiConvenzione", Nome = "DatiConvenzione" },
                    new DataType { Codice = "DatiRicezione", Nome = "DatiRicezione" },
                    new DataType { Codice = "DatiFattureCollegate", Nome = "DatiFattureCollegate" },
                };
            }
        }
    }
}
