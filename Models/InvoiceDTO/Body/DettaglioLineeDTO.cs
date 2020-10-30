using AspNET.Models.InvoiceDTO.Body.Linee;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO.Body
{
    public class DettaglioLineeDTO
    {
        public int Id { get; set; }
        public int BodyModelId { get; set; }
        public int NumeroLinea { get; set; }
        public string TipoCessionePrestazione { get; set; }
        public string Descrizione { get; set; }
        public decimal? Quantita { get; set; }
        public string UnitaMisura { get; set; }
        public DateTime? DataInizioPeriodo { get; set; }
        public DateTime? DataFinePeriodo { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public decimal PrezzoTotale { get; set; }
        public decimal AliquotaIVA { get; set; }
        public string Ritenuta { get; set; }
        public string Natura { get; set; }
        public string RiferimentoAmministrazione { get; set; }
        public List<CodiceArticoloDTO> CodiceArticolo { get; set; }
        public List<ScontoMaggiorazioneDTO> ScontoMaggiorazione { get; set; }
        public List<AltriDatiGestionaliDTO> AltriDatiGestionali { get; set; }
    }
}
