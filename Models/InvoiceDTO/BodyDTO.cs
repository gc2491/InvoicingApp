using AspNET.Models.InvoiceDTO.Body;
using AspNET.Models.InvoiceDTO.Header;
using System;
using System.Collections.Generic;

namespace AspNET.Models.InvoiceDTO
{
    public class BodyDTO
    {
        public int Id { get; set; }
        public int CedentePrestatoreId { get; set; }
        public int CessionarioCommittenteId { get; set; }
        public int? VettoreId { get; set; }
        public int MetadataId { get; set; }

        public string TipoDocumento { get; set; }
        public string Divisa { get; set; }
        public DateTime Data { get; set; }
        public string Numero { get; set; }

        public string BolloVirtuale { get; set; }
        public decimal? ImportoBollo { get; set; }

        public decimal? ImportoTotaleDocumento { get; set; }
        public decimal? Arrotondamento { get; set; }
        public string Art73 { get; set; }

        public string NumeroFatturaPrincipale { get; set; }
        public DateTime? DataFatturaPrincipale { get; set; }

        public string TipoRitenuta { get; set; }
        public decimal? ImportoRitenuta { get; set; }
        public decimal? AliquotaRitenuta { get; set; }
        public string CausalePagamento { get; set; }

        public string MezzoTrasporto { get; set; }
        public string CausaleTrasporto { get; set; }
        public int NumeroColli { get; set; }
        public string Descrizione { get; set; }
        public string UnitaMisuraPeso { get; set; }
        public decimal PesoLordo { get; set; }
        public decimal PesoNetto { get; set; }
        public DateTime DataOraRitiro { get; set; }
        public DateTime DataInizioTrasporto { get; set; }
        public string TipoResa { get; set; }
        public SediDTO LuogoResa { get; set; }
        public DateTime? DataOraConsegna { get; set; }
        public List<CausaleDTO> Causale { get; set; }
        public List<DatiCassaPrevidenzialeDTO> DatiCassaPrevidenziale { get; set; }
        public List<DatiDDTDTO> DatiDDT { get; set; }
        public List<DatiDTO> Dati { get; set; }
        public List<DatiPagamentoDTO> DatiPagamento { get; set; }
        public List<DatiRiepilogoDTO> DatiRiepilogo { get; set; }
        public List<DatiRitenutaDTO> DatiRitenuta { get; set; }
        public List<DatiSALDTO> DatiSAL { get; set; }
        public List<DettaglioLineeDTO> DettaglioLinee { get; set; }
        public CliForDTO CedentePrestatore { get; set; }
        public CliForDTO CessionarioCommittente { get; set; }
        public CliForDTO Vettore { get; set; }
        public MetadataDTO Metadata { get; set; }

        // Fattura Semplificata
        public string NumeroFR { get; set; }
        public DateTime? DataFR { get; set; }
        public string ElementiRettificati { get; set; }
        public List<DatiBeniServiziDTO> DatiBeniServizi { get; set; }
        public bool Simplified { get; set; }
    }
}
